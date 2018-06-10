using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsarr.Library
{
    public static class LibraryServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
