using Microsoft.EntityFrameworkCore;
using Pulsarr.Preferences.DataModel;
using Pulsarr.Preferences.DataModel.Migrations;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.Migrations
{
    public class CreateLibraryTable : IDatabaseMigration
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
                    id INTEGER PRIMARY KEY,
                    added TEXT,
                    state INTEGER,
                    libraryLocation TEXT,
                    description TEXT,
                    dataSourceId TEXT NOT NULL,
                    sourceBookId TEXT NOT NULL,
                    title TEXT,
                    author TEXT,
                    imageUrl TEXT,
                    series TEXT,
                    seriesPart NUMERIC,
                    publicationDate TEXT,
                    Discriminator TEXT
                );
            ");
        }
    }
}
