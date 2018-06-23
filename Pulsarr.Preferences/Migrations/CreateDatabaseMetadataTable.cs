using Microsoft.EntityFrameworkCore;
using Pulsarr.Preferences.DataModel;
using Pulsarr.Preferences.DataModel.Migrations;
using Pulsarr.Preferences.DataModel.Preferences;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.Migrations
{
    public class CreateDatabaseMetadataTable : IDatabaseMigration
    {
        public MigrationPriority Priority { get; } = MigrationPriority.Emergency;

        public bool ShouldRun(string databaseVersion)
        {
            return databaseVersion == null;
        }

        public void Execute(DatabaseStore context)
        {
            context.Database.ExecuteSqlCommand(@"
                create table if not exists Library (
                    key TEXT PRIMARY KEY,
                    value TEXT
                );
            ");
            context.SaveChanges();
            context.DatabaseMetadata.Add(new KeyValuePair("db.version", "1"));
        }
    }
}
