using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Metadata.ServiceInterfaces;
using Pulsarr.Search.Manager.Model;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search.Manager
{
    public class SearchManager : ISearchManager
    {
        private readonly IMetaDataSource _dataSource;
        private readonly IServiceProvider _provider;

        public SearchManager(IMetaDataSource dataSource, IServiceProvider provider)
        {
            _dataSource = dataSource;
            _provider = provider;
        }

        public async Task<List<SearchResult>> SearchByMetaId(string dataSourceId, string bookId)
        {
            var bookInfo = await _dataSource.Lookup(dataSourceId, bookId);
            return await SearchByString(bookInfo.Title);
        }

        public async Task<List<SearchResult>> SearchByString(string query)
        {
            var searchProviders = _provider.GetServices<ISearchProviderFactory>().SelectMany(factory => factory.GetSearchProviders());
            var searchResults = (await Task.WhenAll(searchProviders.Select(p => p.Search(query)))).SelectMany(r => r).ToList();
            // todo: searchResults.Sort();
            return searchResults;
        }
    }
}
