using System;
using System.Collections.Generic;
using System.Linq;
using SabSharp;

namespace Pulsarr.Model.Download.Clients
{
    public class SabnzbdDownloadClient : IDownloadClient
    {
        public DownloadType[] DownloadTypes { get; } = { DownloadType.Usenet };
        private readonly SabClient _client;

        private readonly string _category;

        public SabnzbdDownloadClient(string host, int port, string apiKey, string category)
        {
            _category = category;
            _client = new SabClient(host, (ushort) port, apiKey);
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

        public void Download(Download download)
        {
            var response = _client.AddQueue(download.Uri, "", _category);
            download.ClientMetadata["SabnzbdId"] = response.NzoIds[0];
        }

        public void Abort(Download download)
        {
            _client.DeleteJob((string) download.ClientMetadata["SabnzbdId"], true);
        }

        public bool DownloadEqual(DownloadStateChanged args, Download download)
        {
            return args.ClientId is string s && ((string[]) download.ClientMetadata["SabnzbdId"]).Contains(s);
        }
    }
}
