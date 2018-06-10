using System;
using System.Collections.Generic;
using System.Linq;
using Pulsarr.Download.Model;
using Pulsarr.Download.Model.Events;
using Pulsarr.Download.ServiceInterfaces;
using Pulsarr.Preferences.ServiceInterfaces;
using Transmission.API.RPC;
using Transmission.API.RPC.Entity;

namespace Pulsarr.Download.Clients
{
    public class TransmissionDownloadClient : IDownloadClient
    {
        private enum TorrentStatus
        {
            TR_STATUS_CHECK_WAIT   = (1<<0), /* Waiting in queue to check files */
            TR_STATUS_CHECK        = (1<<1), /* Checking files */
            TR_STATUS_DOWNLOAD     = (1<<2), /* Downloading */
            TR_STATUS_SEED         = (1<<3), /* Seeding */
            TR_STATUS_STOPPED      = (1<<4)  /* Torrent is stopped */
        }

        public DownloadType[] DownloadTypes { get; } = { DownloadType.Torrent };
        private const double BytesInGb = 1024 * 1024 * 1024;

        private readonly Client _client;

        public TransmissionDownloadClient(IPreferenceService preferencesService)
        {
            _client = new Client(
                preferencesService.Get("transmission.host", "127.0.0.1"), null,
                preferencesService.Get("transmission.username", "transmission"),
                preferencesService.Get("transmission.password", "transmission"));
        }

        public IEnumerable<DownloadStateChanged> Poll()
        {
            var torrents = _client.TorrentGet(new []{"status", "sizeWhenDone", "totalSize", "id"});
            var changed = new List<DownloadStateChanged>();
            foreach (var info in torrents.Torrents)
            {
                var status = (TorrentStatus) info.Status;
                DownloadNotificationType type;
                switch (status)
                {
                    case TorrentStatus.TR_STATUS_SEED:
                    case TorrentStatus.TR_STATUS_STOPPED:
                        type = DownloadNotificationType.Done;
                        _client.TorrentRemove(new []{info.ID});
                        break;
                    default:
                        type = DownloadNotificationType.Progress;
                        break;
                }
                var current = info.TotalSize / BytesInGb;
                var complete = (float) (info.SizeWhenDone / BytesInGb);
                if (Math.Abs(complete) < 0.001)
                {
                    complete = 1;
                }
                var percent = (float) Math.Max(100.0, (current / complete) * 100.0);
                changed.Add(new DownloadStateChanged(type, info.ID, percent, complete));
            }
            changed.AddRange(torrents.Removed.Select(removed => new DownloadStateChanged(DownloadNotificationType.Abort, removed.ID)));
            return changed;
        }

        public void Download(DownloadHandle download)
        {
            // logically, "filename" can also be a URL
            var torrent = new NewTorrent {Filename = download.Uri};
            var response = _client.TorrentAdd(torrent);
            download.ClientMetadata["TransmissionTorrentId"] = response.ID;
        }

        public void Abort(DownloadHandle download)
        {
            _client.TorrentRemove(new[] {(int)download.ClientMetadata["TransmissionTorrentId"]}, true);
        }

        public bool DownloadEqual(DownloadStateChanged args, DownloadHandle download)
        {
            return args.ClientId is int i && (int) download.ClientMetadata["TransmissionTorrentId"] == i;
        }
    }
}
