using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsarr.Search.Manager.Model;

namespace Pulsarr.Search.ServiceInterfaces
{
    public interface ISearchProvider : IDisposable
    {
        Task<bool> Test();
        Task<IEnumerable<SearchResult>> Search(string query);
    }
}
