using System.Collections.Generic;
using System.Linq;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.DataStore
{
    public class DatabasePreferenceService : BasePreferenceService
    {
        private readonly IDatabaseManager _manager;

        public override IReadOnlyDictionary<string, string> AllEntries =>
            _manager.PerformDbTask(db => db.Preferences.ToDictionary(k => k.Key, v => v.Value));

        public DatabasePreferenceService(IDatabaseManager manager)
        {
            _manager = manager;
        }

        protected override string this[string key]
        {
            get => _manager.PerformDbTask(db => db.Preferences.Find(key).Value);
            set
            {
                _manager.PerformDbTask(db =>
                {
                    db.Preferences.Find(key).Value = value;
                    return db.SaveChanges();
                });
            }
        }

        protected override void DeleteKeysStartingWith(string key)
        {
            _manager.PerformDbTask(db =>
            {
                db.Preferences.RemoveRange(db.Preferences.Where(kv => kv.Key.StartsWith(key + ".")));
                return true;
            });
        }
    }
}
