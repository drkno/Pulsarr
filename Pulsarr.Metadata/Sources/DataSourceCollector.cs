using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Metadata.Model;
using Pulsarr.Metadata.ServiceInterfaces;

namespace Pulsarr.Metadata.Sources
{
    public class DataSourceCollector : IMetaDataSource
    {
        private readonly IServiceProvider _provider;

        public DataSourceCollector(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<ImmutableList<Book>> Search(string title)
        {
            var resultList = new List<Book>();
            if (string.IsNullOrWhiteSpace(title))
            {
                return resultList.ToImmutableList();
            }
            var dataSources = _provider.GetServices<IDataSource>();
            foreach (var dataSource in dataSources.Where(d => d.Enabled))
            {
                var results = await dataSource.Search(title);
                resultList.AddRange(results);
            }
            return resultList.ToImmutableList();
        }

        public Task<Book> Lookup(string dataSourceId, string bookId)
        {
            var dataSource = _provider.GetServices<IDataSource>().First(p => p.Enabled && p.SourceId == dataSourceId);
            return dataSource.Lookup(bookId);
        }
    }
}
