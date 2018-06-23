using Pulsarr.Preferences.DataModel;
using Pulsarr.Preferences.DataModel.Migrations;

namespace Pulsarr.Preferences.ServiceInterfaces
{
    public interface IDatabaseMigration
    {
        MigrationPriority Priority { get; }
        bool ShouldRun(string databaseVersion);
        void Execute(DatabaseStore context);
    }
}
