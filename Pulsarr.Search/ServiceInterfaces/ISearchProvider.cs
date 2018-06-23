using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsarr.Search.Manager.Model;

namespace Pulsarr.Search.ServiceInterfaces
{
    interface ISearchProvider
    {
        Task<IEnumerable<SearchResult>> Search(string query);
    }
}
