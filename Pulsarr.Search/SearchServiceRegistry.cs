using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Search.Client.Newznab;
using Pulsarr.Search.Manager;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search
{
    public static class SearchServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISearchManager, SearchManager>();
            services.AddSingleton<ISearchProviderFactory, NewznabFactory>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
