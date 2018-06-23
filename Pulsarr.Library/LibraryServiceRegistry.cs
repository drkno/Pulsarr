using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Library.ServiceInterfaces;

namespace Pulsarr.Library
{
    public static class LibraryServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILibraryService, LibraryService>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
