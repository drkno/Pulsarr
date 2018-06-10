using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Download.Model;
using Pulsarr.Download.ServiceInterfaces;

namespace Pulsarr.Download.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadManager _manager;

        public DownloadController(IDownloadManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IEnumerable<DownloadHandle> Get()
        {
            return _manager.ActiveDownloads;
        }

        [HttpGet("{id}")]
        public DownloadHandle Get(string id)
        {
            return _manager.ActiveDownloads.FirstOrDefault(d => d.Id == id);
        }

        [HttpPost]
        public DownloadHandle Post([FromBody] DownloadPostData postDataData)
        {
            return _manager.DownloadItem(postDataData.Type, postDataData.Uri);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _manager.CancelDownload(id);
        }
    }
}
