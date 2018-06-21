using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsarr.Search.Manager.Model;

namespace Pulsarr.Search.ServiceInterfaces
{
    public interface ISearchManager
    {
        Task<IEnumerable<SearchResult>> SearchByMetaId(string dataSourceId, string bookId);
        Task<IEnumerable<SearchResult>> SearchByString(string query);
    }
}
