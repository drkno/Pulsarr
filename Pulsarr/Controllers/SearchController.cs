using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Metadata.Model;
using Pulsarr.Metadata.ServiceInterfaces;

namespace Pulsarr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMetaDataSource _dataSource;

        public SearchController(IMetaDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [HttpGet]
        public string GetHelp()
        {
            return "This endpoint only supports a POST based search query or a GET at ./{sourceId}/{bookId}";
        }

        [HttpPost("{title}")]
        public async Task<ImmutableList<Book>> SearchForBook([FromBody] string title)
        {
            return await _dataSource.Search(title);
        }

        [HttpGet("{sourceId}/{bookId}")]
        public async Task<Book> LookupBook(string sourceId, string bookId)
        {
            return await _dataSource.Lookup(sourceId, bookId);
        }
    }
}
