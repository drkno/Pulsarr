using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Metadata.ServiceInterfaces;
using Pulsarr.Metadata.Sources;
using Pulsarr.Metadata.Sources.GoodReads;

namespace Pulsarr.Metadata
{
    public static class MetadataServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataSource, GoodReadsDataSource>();
            services.AddSingleton<IMetaDataSource, DataSourceCollector>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
