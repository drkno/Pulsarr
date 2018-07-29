using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Search.Manager.Model;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchManager _searchManager;

        public SearchController(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        [HttpPost]
        public Task<List<SearchResult>> SearchByString([FromBody] string query)
        {
            return _searchManager.SearchByString(query);
        }

        [HttpPost("{metadatId}/{bookId}")]
        public Task<List<SearchResult>> SearchByMetaId(string metadatId, string bookId)
        {
            return _searchManager.SearchByMetaId(metadatId, bookId);
        }
    }
}
