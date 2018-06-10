using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Download.Clients;
using Pulsarr.Download.Manager;
using Pulsarr.Download.ServiceInterfaces;

namespace Pulsarr.Download
{
    public static class DownloadServiceRegistry
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // clients
            services.AddSingleton<IDownloadClient, SabnzbdDownloadClient>();
            services.AddSingleton<IDownloadClient, SabnzbdDownloadClient>();

            // download manager
            services.AddSingleton<IDownloadManager, DownloadManager>();
        }
    }
}
