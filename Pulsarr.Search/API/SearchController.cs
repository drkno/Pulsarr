using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Search.Client.Newznab;
using Pulsarr.Search.Client.Newznab.Model;

namespace Pulsarr.Search.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<NewzNabCapabilities> Get()
        {
//            var client = new HttpClient();
//            var resp = await client.GetAsync("");
////            return await resp.Content.ReadAsAsync<Capabilities>();
//            return await resp.Content.ReadAsStringAsync();
        }
    }
}
