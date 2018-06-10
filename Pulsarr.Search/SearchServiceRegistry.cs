using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsarr.Search
{
    public static class SearchServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
