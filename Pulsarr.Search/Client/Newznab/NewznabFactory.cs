using System.Collections.Generic;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search.Client.Newznab
{
    internal class NewznabFactory : ISearchProviderFactory
    {
        public IEnumerable<ISearchProvider> GetSearchProviders()
        {
            return null;
        }
    }
}
