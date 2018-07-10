using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Authorisation.ServiceInterfaces;

namespace Pulsarr.Authorisation
{
    public static class AuthorisationServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthorisationService, AuthorisationService>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
