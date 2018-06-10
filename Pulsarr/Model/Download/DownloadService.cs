using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Pulsarr.Model.Download.Clients;
using Pulsarr.Model.Preferences;

namespace Pulsarr.Model.Download
{
    public class DownloadService : IDisposable
    {
        private readonly IDownloadClient[] _downloadClients;
        private readonly Random _random;
        private readonly List<Download> _downloads;
        private readonly Timer _pollTimer;

        public event EventHandler<DownloadEventArgs> DownloadStarted;
        public event EventHandler<IEnumerable<DownloadEventArgs>> DownloadFinished;
        public event EventHandler<IEnumerable<DownloadEventArgs>> DownloadProgress;

        public DownloadService(PreferencesService preferencesService)
        {
            _downloadClients = new IDownloadClient[] {
                new SabnzbdDownloadClient(preferencesService["sabnzbd.host"], preferencesService.Get("sabnzbd.port"), preferencesService["sabnzbd.apikey"], preferencesService["sabnzbd.category"]),
                new TransmissionDownloadClient(preferencesService["transmission.host"], preferencesService["transmission.username"], preferencesService["transmission.password"])
            };
            _random = new Random();
            _downloads = new List<Download>();
            _pollTimer = new Timer(ExecuteDownloadClientPoll, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        }

        private void ExecuteDownloadClientPoll(object timerState)
        {
            var seen = new List<string>();
            var complete = new List<DownloadEventArgs>();
            var progress = new List<DownloadEventArgs>();

            foreach (var downloadClient in _downloadClients)
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

        public IEnumerable<Download> GetCurrentDownloads()
        {
            return _downloads.AsReadOnly();
        }

        public Download DownloadItem(DownloadType downloadType, string downloadUri)
        {
            var clients = _downloadClients.Where(d => d.DownloadTypes.Contains(downloadType)).ToArray();
            var client = _downloadClients[_random.Next(0, clients.Length)];
            var correlationId = Guid.NewGuid().ToString("N");
            var download = new Download(client, downloadType, downloadUri, correlationId);
            client.Download(download);
            DownloadStarted?.Invoke(this, new DownloadEventArgs(download, DownloadNotificationType.Start));
            return download;
        }

        public void CancelDownload(string correlationId)
        {
            var download = _downloads.Find(d => d.Id == correlationId);
            CancelDownload(download);
        }

        public void CancelDownload(Download download)
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
