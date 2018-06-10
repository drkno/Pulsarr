using System;

namespace Pulsarr.Model.Download
{
    public class DownloadEventArgs : EventArgs
    {
        public Download Download { get; }
        public DownloadNotificationType State { get; }

        public DownloadEventArgs(Download download, DownloadNotificationType state)
        {
            Download = download;
            State = state;
        }
    }
}
