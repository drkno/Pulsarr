using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NzbDrone.Core.MetadataSource.SkyHook.Resource;
using Pulsarr.Model.Metadata;

namespace Pulsarr.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {      
        [HttpPost("{title}")]
        public async Task<ImmutableList<IBook>> SearchForBook(string title)
        {
            return await DataSourceCollector.Instance.Search(title);
        }

        [HttpGet("{id}")]
        public async Task<IBook> LookupBook(ulong id)
        {
            return await DataSourceCollector.Instance.Lookup(id);
        }

        // http://localhost:54572/api/v1/search?type=artist&query=test
        [HttpGet]
        public async Task<List<AlbumResource>> Search(
            [FromQuery(Name = "type")] string type,
            [FromQuery(Name = "query")] string query)
        {
            var data = await DataSourceCollector.Instance.Search(query);
            var resources = new List<AlbumResource>();
            foreach (var d in data)
            {
                var ar = new AlbumResource();
                ar.Title = d.Title;
                ar.Artist = new AlbumArtistResource() {Id = "", Name = d.Author};
                ar.Id = d.SourceId + "-" + d.SourceBookId;
                ar.Images = new List<ImageResource>() {new ImageResource() {CoverType = "Poster", Url = d.ImageUrl}};
                resources.Add(ar);
            }
            return resources;
        }
    }
}
