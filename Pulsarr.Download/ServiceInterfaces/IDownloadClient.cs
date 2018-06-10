using System.Collections.Generic;
using Pulsarr.Download.Model;
using Pulsarr.Download.Model.Events;

namespace Pulsarr.Download.ServiceInterfaces
{
    public interface IDownloadClient
    {
        bool Enabled { get; }
        DownloadType[] DownloadTypes { get; }
        IEnumerable<DownloadStateChanged> Poll();
        void Download(DownloadHandle download);
        void Abort(DownloadHandle download);
        bool DownloadEqual(DownloadStateChanged args, DownloadHandle download);
    }
}
