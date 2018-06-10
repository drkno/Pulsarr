using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Download.Clients;
using Pulsarr.Download.Manager;
using Pulsarr.Download.ServiceInterfaces;

namespace Pulsarr.Download
{
    public static class DownloadServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDownloadClient, SabnzbdDownloadClient>();
            services.AddSingleton<IDownloadClient, SabnzbdDownloadClient>();
            services.AddSingleton<IDownloadManager, DownloadManager>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
