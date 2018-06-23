using System.Collections.Generic;

namespace Pulsarr.Search.ServiceInterfaces
{
    interface ISearchProviderFactory
    {
        IEnumerable<ISearchProvider> GetSearchProviders();
    }
}
