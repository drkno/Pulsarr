using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Preferences.DataStore;
using Pulsarr.Preferences.Migrations;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences
{
    public static class PreferencesServiceRegistry
    {
        public static Assembly ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPreferenceService, DatabasePreferenceService>();
            services.AddSingleton<IDatabaseManager, DatabaseManager>();
            services.AddTransient<IDatabaseMigration, DevelopmentMigration>();
            services.AddTransient<IDatabaseMigration, CreateLibraryTable>();
            services.AddTransient<IDatabaseMigration, CreatePreferencesTable>();
            services.AddTransient<IDatabaseMigration, CreateDatabaseMetadataTable>();
            return Assembly.GetExecutingAssembly();
        }
    }
}
