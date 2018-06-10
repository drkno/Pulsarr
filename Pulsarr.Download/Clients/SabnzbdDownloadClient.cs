using System;
using System.Collections.Generic;
using System.Linq;
using Pulsarr.Download.Model;
using Pulsarr.Download.Model.Events;
using Pulsarr.Download.ServiceInterfaces;
using Pulsarr.Preferences.ServiceInterfaces;
using SabSharp;

namespace Pulsarr.Download.Clients
{
    public class SabnzbdDownloadClient : IDownloadClient
    {
        public DownloadType[] DownloadTypes { get; } = { DownloadType.Usenet };
        private readonly SabClient _client;

        private readonly string _category;

        public SabnzbdDownloadClient(IPreferenceService preferencesService)
        {
            _category = preferencesService.Get("sabnzbd.category", "audiobooks");
            _client = new SabClient(
                preferencesService.Get("sabnzbd.host", "127.0.0.1"),
                preferencesService.Get("sabnzbd.port", (ushort) 8080),
                preferencesService.Get("sabnzbd.apikey", ""));
        }

        public IEnumerable<DownloadStateChanged> Poll()
        {
            var changes = new List<DownloadStateChanged>();
            var response = _client.GetQueue();
            foreach (var slot in response.Queue.Slots)
            {
                if (slot.Cat != _category)
                {
                    continue;
                }

                float sizeGb, percentage;
                if (!float.TryParse(slot.Mb, out var mbs))
                {
                    sizeGb = -1;
                    percentage = -1;
                }
                else
                {
                    sizeGb = mbs / 1024f;
                    percentage = (float)Math.Max((mbs / float.Parse(slot.Mbleft)) * 100.0f, 100.0);
                }
                if (slot.Status == "Completed")
                {
                    _client.DeleteJob(slot.NzoId, false);
                    changes.Add(new DownloadStateChanged(DownloadNotificationType.Done, slot.NzoId, 100.0f, sizeGb));
                }
                else
                {
                    changes.Add(new DownloadStateChanged(DownloadNotificationType.Progress, slot.NzoId, percentage, sizeGb));
                }
            }
            return changes;
        }

        public void Download(DownloadHandle download)
        {
            var response = _client.AddQueue(download.Uri, "", _category);
            download.ClientMetadata["SabnzbdId"] = response.NzoIds[0];
        }

        public void Abort(DownloadHandle download)
        {
            _client.DeleteJob((string) download.ClientMetadata["SabnzbdId"], true);
        }

        public bool DownloadEqual(DownloadStateChanged args, DownloadHandle download)
        {
            return args.ClientId is string s && ((string[]) download.ClientMetadata["SabnzbdId"]).Contains(s);
        }
    }
}
