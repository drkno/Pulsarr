using System.Collections.Generic;
using Pulsarr.Download.ServiceInterfaces;

namespace Pulsarr.Download.Model
{
    public class DownloadHandle
    {
        public DownloadType Type { get; }
        public string Uri { get; }
        public IDownloadClient Client { get; }
        public string Id { get; }
        public float Percentage { get; set; } = -1;
        public float TotalGb { get; set; } = -1;
        public Dictionary<string, object> ClientMetadata { get; }

        public DownloadHandle(IDownloadClient client, DownloadType downloadType, string downloadUri, string correlationId)
        {
            Type = downloadType;
            Uri = downloadUri;
            Client = client;
            Id = correlationId;
            ClientMetadata = new Dictionary<string, object>();
        }
    }
}
