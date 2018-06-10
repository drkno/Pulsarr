using System;
using System.Collections.Generic;
using Pulsarr.Download.Model;
using Pulsarr.Download.Model.Events;

namespace Pulsarr.Download.ServiceInterfaces
{
    public interface IDownloadManager : IDisposable
    {
        event EventHandler<DownloadEventArgs> DownloadStarted;
        event EventHandler<IEnumerable<DownloadEventArgs>> DownloadFinished;
        event EventHandler<IEnumerable<DownloadEventArgs>> DownloadProgress;
        
        IEnumerable<DownloadHandle> ActiveDownloads { get; }

        DownloadHandle DownloadItem(DownloadType downloadType, string downloadUri);
        void CancelDownload(string correlationId);
        void CancelDownload(DownloadHandle download);
    }
}
