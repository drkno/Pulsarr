using Microsoft.EntityFrameworkCore;
using Pulsarr.Preferences.DataModel;
using Pulsarr.Preferences.DataModel.Migrations;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.Migrations
{
    public class CreatePreferencesTable : IDatabaseMigration
    {
        public MigrationPriority Priority { get; } = MigrationPriority.Emergency;

        public bool ShouldRun(string databaseVersion)
        {
            return databaseVersion == null;
        }

        public void Execute(DatabaseStore context)
        {
            context.Database.ExecuteSqlCommand(@"
                create table if not exists Preferences (
                    key TEXT PRIMARY KEY,
                    value TEXT
                );
            ");
        }
    }
}
