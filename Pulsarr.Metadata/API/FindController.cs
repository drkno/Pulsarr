using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Metadata.Model;
using Pulsarr.Metadata.ServiceInterfaces;

namespace Pulsarr.Metadata.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly IMetaDataSource _dataSource;

        public FindController(IMetaDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [HttpGet]
        public string GetHelp()
        {
            return "This endpoint only supports a POST based search query or a GET at ./{sourceId}/{bookId}";
        }

        [HttpPost]
        public async Task<ImmutableList<Book>> SearchForBook([FromBody] FindPostData postDataData)
        {
            return await _dataSource.Search(postDataData.Title);
        }

        [HttpGet("{sourceId}/{bookId}")]
        public async Task<Book> LookupBook(string sourceId, string bookId)
        {
            return await _dataSource.Lookup(sourceId, bookId);
        }
    }
}
