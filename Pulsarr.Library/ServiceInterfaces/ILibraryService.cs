using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsarr.Preferences.DataModel.Library;

namespace Pulsarr.Library.ServiceInterfaces
{
    public interface ILibraryService
    {
        IList<LibraryItem> GetAll();
        Task<LibraryItem> Get(ulong libraryId);
        void Delete(ulong libraryId);
        Task<LibraryItem> Add(string dataSourceId, string bookId);
        Task<LibraryItem> RefreshMetadata(ulong libraryId);
    }
}
