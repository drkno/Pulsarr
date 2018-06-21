using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsarr.Search.Manager.Model;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search.Manager
{
    public class SearchManager : ISearchManager
    {
        public async Task<IEnumerable<SearchResult>> SearchByMetaId(string dataSourceId, string bookId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<SearchResult>> SearchByString(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}
