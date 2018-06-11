using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Search.Client.Newznab.Model;

namespace Pulsarr.Search.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            var client = new HttpClient();
            var resp = await client.GetAsync(
                "https://jackett.pms.makereti.co.nz/api/v2.0/indexers/horriblesubs/results/torznab/apiapi?t=caps&apikey=pan6scbplhq91odzxc7e1mswobnp1hxx");
//            return await resp.Content.ReadAsAsync<Capabilities>();
            return await resp.Content.ReadAsStringAsync();
        }
    }
}
