using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Preferences.ServiceInterfaces;
using Pulsarr.Preferences.Stores;

namespace Pulsarr.Preferences
{
    public static class PreferencesServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPreferenceService, DummyPreferenceService>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
