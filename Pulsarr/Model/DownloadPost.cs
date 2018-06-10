using Pulsarr.Download.Model;

namespace Pulsarr.Model
{
    public class DownloadPost
    {
        public DownloadType Type { get; set; }
        public string Uri { get; set; }
    }
}
