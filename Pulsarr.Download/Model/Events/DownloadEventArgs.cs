using System;

namespace Pulsarr.Download.Model.Events
{
    public class DownloadEventArgs : EventArgs
    {
        public DownloadHandle Download { get; }
        public DownloadNotificationType State { get; }

        public DownloadEventArgs(DownloadHandle download, DownloadNotificationType state)
        {
            Download = download;
            State = state;
        }
    }
}
