using System.Collections.Generic;
using System.Linq;
using Pulsarr.Preferences.ServiceInterfaces;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search.Client.Newznab
{
    public class NewznabFactory : ISearchProviderFactory
    {
        private readonly IPreferenceService _preferenceService;
        private readonly List<SearchProviderCacheItem> _searchProviders;

        public NewznabFactory(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
            _searchProviders = new List<SearchProviderCacheItem>();
        }

        public IEnumerable<ISearchProvider> GetSearchProviders()
        {
            var indexerPreferences = _preferenceService.GetObjectArray<IndexerPreferences>("indexer.newznab");

            var removed = _searchProviders.Select(i => i.Preferences).Except(indexerPreferences).Select(i => _searchProviders.Find(p => p.Preferences == i)).ToArray();
            foreach (var remove in removed)
            {
                remove.SearchProvider.Dispose();
                _searchProviders.Remove(remove);
            }

            var added = indexerPreferences.Except(_searchProviders.Select(i => i.Preferences));
            foreach (var add in added)
            {
                var provider = new NewzNabClient(add);
                _searchProviders.Add(new SearchProviderCacheItem {Preferences = add, SearchProvider = provider});
            }

            return _searchProviders.Where(p => p.Preferences.Enabled).Select(i => i.SearchProvider);
        }

        private class SearchProviderCacheItem
        {
            public IndexerPreferences Preferences;
            public ISearchProvider SearchProvider;
        }
    }
}
