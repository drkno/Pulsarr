using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Metadata.ServiceInterfaces;
using Pulsarr.Metadata.Sources;
using Pulsarr.Metadata.Sources.GoodReads;

namespace Pulsarr.Metadata
{
    public static class MetadataServiceRegistry
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // data sources
            services.AddSingleton<IDataSource, GoodReadsDataSource>();

            services.AddSingleton<IMetaDataSource, DataSourceCollector>();
        }
    }
}
