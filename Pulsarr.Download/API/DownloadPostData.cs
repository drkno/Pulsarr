using Pulsarr.Download.Model;

namespace Pulsarr.Download.API
{
    public class DownloadPostData
    {
        public DownloadType Type { get; set; }
        public string Uri { get; set; }
    }
}
