using System.Collections.Generic;
using System.Linq;
using Pulsarr.Preferences.DataModel.Preferences;
using Pulsarr.Preferences.Exceptions;
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
            get
            {
                var kvp = _manager.PerformDbTask(db => db.Preferences.Find(key));
                if (kvp == null)
                {
                    throw new NoSuchPreferenceException();
                }
                return kvp.Value;
            }
            set
            {
                _manager.PerformDbTask(db =>
                {
                    var kvp = db.Preferences.Find(key);
                    if (kvp != null)
                    {
                        kvp.Value = value;
                    }
                    else
                    {
                        db.Preferences.Add(new PreferenceKeyValuePair(key, value));
                    }
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
