using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Library.Model;
using Pulsarr.Library.ServiceInterfaces;
using Pulsarr.Preferences.DataModel.Library;

namespace Pulsarr.Library.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public LibraryItemsResponse GetLibraryItems([FromQueryAttribute] string sortBy = null,
                                                    [FromQueryAttribute] SortOrder sortOrder = SortOrder.Ascending,
                                                    [FromQueryAttribute] ulong maxResults = 0,
                                                    [FromQueryAttribute] ulong startAt = 0)
        {
            var items = _libraryService.GetAll();
            return new LibraryItemsResponse(items, sortBy, sortOrder, maxResults, startAt);
        }
        
        [HttpGet("{libraryId}")]
        public async Task<LibraryItem> GetLibraryItem(ulong libraryId,
                                                      [FromQueryAttribute] bool forceRefresh)
        {
            if (forceRefresh)
            {
                return await _libraryService.RefreshMetadata(libraryId);
            }
            return await _libraryService.Get(libraryId);
        }

        [HttpDelete("{libraryId}")]
        public void DeleteLibraryItem(ulong libraryId)
        {
            _libraryService.Delete(libraryId);
        }

        [HttpPost("{dataSourceId}/{bookId}")]
        public async Task<LibraryItem> AddLibraryItem(string dataSourceId,
                                                      string bookId)
        {
            return await _libraryService.Add(dataSourceId, bookId);
        }
    }
}
