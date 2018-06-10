using System;

namespace Pulsarr.Download.Model.Events
{
    public class DownloadStateChanged : EventArgs
    {
        public DownloadStateChanged(DownloadNotificationType state, object clientId, float percentage = -1, float totalGb = -1)
        {
            State = state;
            Percentage = percentage;
            TotalGb = totalGb;
            ClientId = clientId;
        }

        public DownloadNotificationType State { get; }
        public float Percentage { get; }
        public float TotalGb { get; }
        public object ClientId { get; }
    }
}
