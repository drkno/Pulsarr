using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Preferences.DataModel;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.DataStore
{
    public class DatabaseManager : IDatabaseManager
    {
        public DatabaseManager(IServiceProvider provider)
        {
            var migrations = new List<IDatabaseMigration>(provider.GetServices<IDatabaseMigration>());
            migrations.Sort((m1, m2) => m2.Priority - m1.Priority);
            using (var db = new DatabaseStore())
            {
                db.Database.Migrate();
                string version;
                try
                {
                    version = db.DatabaseMetadata.Find("db.version").Value;
                }
                catch
                {
                    version = null;
                }

                foreach (var databaseMigration in migrations)
                {
                    if (databaseMigration.ShouldRun(version))
                    {
                        databaseMigration.Execute(db);
                    }
                }
                db.SaveChanges();
            }
        }

        public T PerformDbTask<T>(Func<DatabaseStore, T> dbUser)
        {
            using (var db = new DatabaseStore())
            {
                return dbUser(db);
            }
        }

        public Task<T> PerformDbTaskAsync<T>(Func<DatabaseStore, Task<T>> dbUser)
        {
            using (var db = new DatabaseStore())
            {
                return dbUser(db);
            }
        }
    }
}
