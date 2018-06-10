using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsarr.PostDownload
{
    public static class PostDownloadServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
