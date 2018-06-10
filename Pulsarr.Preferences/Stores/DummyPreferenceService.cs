using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Pulsarr.Preferences.Exceptions;

namespace Pulsarr.Preferences.Stores
{
    public class DummyPreferenceService : BasePreferenceService
    {
        private readonly Dictionary<string, string> _hardCodedPreferences;

        public DummyPreferenceService()
        {
            _hardCodedPreferences = new Dictionary<string, string>();
            if (File.Exists("devpreferences.properties"))
            {
                var data = File.ReadAllText("devpreferences.properties");
                var lines = data.Split(new string[] {"\r\n", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var lineWithoutComment = line;
                    if (lineWithoutComment.Contains("#"))
                    {
                        lineWithoutComment = lineWithoutComment.Substring(0, lineWithoutComment.IndexOf('#'));
                    }

                    if (!lineWithoutComment.Contains("="))
                    {
                        continue;
                    }

                    var index = lineWithoutComment.IndexOf('=');
                    var key = lineWithoutComment.Substring(0, index);
                    var val = index + 1 < lineWithoutComment.Length ? lineWithoutComment.Substring(index + 1) : "";
                    if (_hardCodedPreferences.ContainsKey(key))
                    {
                        throw new DuplicateNameException("Duplicate key names are not allowed. Please check your devpreferences.properties");
                    }
                    _hardCodedPreferences[key] = val;
                }
            }
        }

        protected override string this[string key]
        {
            get
            {
                if (!_hardCodedPreferences.ContainsKey(key))
                {
                    throw new NoSuchPreferenceException();
                }
                return _hardCodedPreferences[key];
            }
            set => _hardCodedPreferences[key] = value;
        }
    }
}
