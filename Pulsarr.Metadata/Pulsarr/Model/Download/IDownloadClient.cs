using System;
using System.Collections.Generic;

namespace Pulsarr.Model.Download
{
    public interface IDownloadClient
    {
        DownloadType[] DownloadTypes { get; }
        IEnumerable<DownloadStateChanged> Poll();
        void Download(Download download);
        void Abort(Download download);
        bool DownloadEqual(DownloadStateChanged args, Download download);
    }
}
