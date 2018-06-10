using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Download.Model;
using Pulsarr.Download.Model.Events;
using Pulsarr.Download.ServiceInterfaces;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Download.Manager
{
    public class DownloadManager : IDownloadManager
    {
        private readonly IServiceProvider _provider;
        private readonly Random _random;
        private readonly List<DownloadHandle> _downloads;
        private readonly Timer _pollTimer;

        public event EventHandler<DownloadEventArgs> DownloadStarted;
        public event EventHandler<IEnumerable<DownloadEventArgs>> DownloadFinished;
        public event EventHandler<IEnumerable<DownloadEventArgs>> DownloadProgress;
        public IEnumerable<DownloadHandle> ActiveDownloads => _downloads.AsReadOnly();

        public DownloadManager(IPreferenceService preferencesService, IServiceProvider provider)
        {
            _provider = provider;
            _random = new Random();
            _downloads = new List<DownloadHandle>();
            _pollTimer = new Timer(
                ExecuteDownloadClientPoll,
                null,
                TimeSpan.Zero,
                TimeSpan.FromMinutes(preferencesService.Get("downloadmanager.pollmins", 2)));
        }

        private void ExecuteDownloadClientPoll(object timerState)
        {
            var seen = new List<string>();
            var complete = new List<DownloadEventArgs>();
            var progress = new List<DownloadEventArgs>();
            
            var downloadClients = _provider.GetServices<IDownloadClient>();
            foreach (var downloadClient in downloadClients.Where(d => d.Enabled))
            {
                var changes = downloadClient.Poll();
                foreach (var change in changes)
                {
                    var download = _downloads.FirstOrDefault(d => d.Client.DownloadEqual(change, d));
                    if (download == null)
                    {
                        continue;
                    }
                    switch (change.State)
                    {
                        case DownloadNotificationType.Done:
                            _downloads.Remove(download);
                            complete.Add(new DownloadEventArgs(download, DownloadNotificationType.Done));
                            break;
                        case DownloadNotificationType.Progress:
                            download.Percentage = change.Percentage;
                            download.TotalGb = change.TotalGb;
                            progress.Add(new DownloadEventArgs(download, DownloadNotificationType.Progress));
                            break;
                    }
                }
            }

            DownloadProgress?.Invoke(this, progress);

            complete.AddRange(_downloads.Where(d => !seen.Contains(d.Id)).Select(d => new DownloadEventArgs(d, DownloadNotificationType.Abort)));
            foreach (var completedDownload in complete)
            {
                _downloads.Remove(completedDownload.Download);
            }
            DownloadFinished?.Invoke(this, complete);
        }

        public DownloadHandle DownloadItem(DownloadType downloadType, string downloadUri)
        {
            var downloadClients = _provider.GetServices<IDownloadClient>();
            var clients = downloadClients.Where(d => d.DownloadTypes.Contains(downloadType)).ToArray();
            var client = clients[_random.Next(0, clients.Length)];
            var correlationId = Guid.NewGuid().ToString("N");
            var download = new DownloadHandle(client, downloadType, downloadUri, correlationId);
            client.Download(download);
            DownloadStarted?.Invoke(this, new DownloadEventArgs(download, DownloadNotificationType.Start));
            return download;
        }

        public void CancelDownload(string correlationId)
        {
            var download = _downloads.Find(d => d.Id == correlationId);
            CancelDownload(download);
        }

        public void CancelDownload(DownloadHandle download)
        {
            var client = download.Client;
            client.Abort(download);
            DownloadStarted?.Invoke(this, new DownloadEventArgs(download, DownloadNotificationType.Abort));
        }

        public void Dispose()
        {
            _pollTimer?.Dispose();
        }
    }
}
