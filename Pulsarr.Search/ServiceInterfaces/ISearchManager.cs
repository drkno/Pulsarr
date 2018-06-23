using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsarr.Search.Manager.Model;

namespace Pulsarr.Search.ServiceInterfaces
{
    public interface ISearchManager
    {
        Task<List<SearchResult>> SearchByMetaId(string dataSourceId, string bookId);
        Task<List<SearchResult>> SearchByString(string query);
    }
}
