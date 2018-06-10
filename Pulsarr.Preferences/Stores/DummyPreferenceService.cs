using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.IO;
using Pulsarr.Preferences.Exceptions;

// ReSharper disable InconsistentlySynchronizedField
namespace Pulsarr.Preferences.Stores
{
    public class DummyPreferenceService : BasePreferenceService
    {
        private const string PreferencesFile = "devpreferences.properties";
        private readonly IDictionary<string, string> _preferences;
        private readonly IDictionary<string, string> _comments;

        public override IEnumerable<string> Keys => _preferences.Keys;
        public override IReadOnlyDictionary<string, string> AllEntries => _preferences.ToImmutableDictionary();

        public DummyPreferenceService()
        {
            _preferences = new ConcurrentDictionary<string, string>();
            _comments = new Dictionary<string, string>();
            if (File.Exists(PreferencesFile))
            {
                LoadPreferencesFromFile();
            }
        }

        private void LoadPreferencesFromFile()
        {
            var data = File.ReadAllText(PreferencesFile);
            var lines = data.Split(new[] {"\r\n", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parsableLine = line;
                string comment = null;
                if (parsableLine.Contains("#"))
                {
                    var commentIndex = parsableLine.IndexOf('#');
                    comment = commentIndex + 1 < parsableLine.Length ? parsableLine.Substring(commentIndex + 1) : "";
                    parsableLine = parsableLine.Substring(0, commentIndex);
                }

                if (!parsableLine.Contains("="))
                {
                    continue;
                }

                var index = parsableLine.IndexOf('=');
                var key = parsableLine.Substring(0, index);
                var val = index + 1 < parsableLine.Length ? parsableLine.Substring(index + 1) : "";
                if (_preferences.ContainsKey(key))
                {
                    throw new DuplicateNameException("Duplicate key names are not allowed. Please check your devpreferences.properties");
                }
                _preferences[key] = val;
                if (!string.IsNullOrWhiteSpace(comment))
                {
                    _comments[key] = comment;
                }
            }
        }

        private void SavePreferencesToFile()
        {
            using (var writer = new StreamWriter(PreferencesFile))
            {
                foreach (var preference in _preferences.Keys)
                {
                    var comment = _comments.ContainsKey(preference) ? $"# {_comments[preference]}" : "";
                    writer.WriteLine($"{preference}={_preferences[preference]}{comment}");
                }
            }
        }

        protected override string this[string key]
        {
            get
            {
                lock (_preferences)
                {
                    if (!_preferences.ContainsKey(key))
                    {
                        throw new NoSuchPreferenceException();
                    }
                    return _preferences[key];
                }
            }
            set
            {
                lock (_preferences)
                {
                    _preferences[key] = value;
                    SavePreferencesToFile();
                }
            }
        }
    }
}
