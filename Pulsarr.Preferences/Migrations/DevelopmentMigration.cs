using System;
using System.IO;
using Pulsarr.Preferences.DataModel;
using Pulsarr.Preferences.DataModel.Migrations;
using Pulsarr.Preferences.DataModel.Preferences;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.Migrations
{
    public class DevelopmentMigration : IDatabaseMigration
    {
        private const string PreferencesFile = "devpreferences.properties";

        public MigrationPriority Priority { get; } = MigrationPriority.High;

        public bool ShouldRun(string databaseVersion)
        {
            return File.Exists(PreferencesFile);
        }

        public void Execute(DatabaseStore context)
        {
            var data = File.ReadAllText(PreferencesFile);
            var lines = data.Split(new[] {"\r\n", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parsableLine = line;
                if (parsableLine.Contains("#"))
                {
                    var commentIndex = parsableLine.IndexOf('#');
                    parsableLine = parsableLine.Substring(0, commentIndex);
                }

                if (!parsableLine.Contains("="))
                {
                    continue;
                }

                var index = parsableLine.IndexOf('=');
                var key = parsableLine.Substring(0, index);
                var val = index + 1 < parsableLine.Length ? parsableLine.Substring(index + 1) : "";

                var kvp = context.Preferences.Find(key);
                if (kvp != null)
                {
                    kvp.Value = val;
                }
                else
                {
                    context.Add(new KeyValuePair(key, val));
                }
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}
