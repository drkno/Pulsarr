using System.Collections.Generic;

namespace Pulsarr.Model.Download
{
    public class Download
    {
        public DownloadType Type { get; }
        public string Uri { get; }
        public IDownloadClient Client { get; }
        public string Id { get; }
        public float Percentage { get; set; } = -1;
        public float TotalGb { get; set; } = -1;
        public Dictionary<string, object> ClientMetadata { get; }

        public Download(IDownloadClient client, DownloadType downloadType, string downloadUri, string correlationId)
        {
            Type = downloadType;
            Uri = downloadUri;
            Client = client;
            Id = correlationId;
            ClientMetadata = new Dictionary<string, object>();
        }
    }
}
