using Pulsarr.Preferences.DataModel.Preferences;
using System;
using System.Collections.Generic;

namespace Pulsarr.Search.Client
{
    public class IndexerPreferences : IEquatable<IndexerPreferences>
    {
        [Preference]
        public bool Enabled { get; set; }
        [Preference]
        public bool UseTls { get; set; }
        [Preference]
        public string Host { get; set; }
        [Preference]
        public string Resource { get; set; }
        [Preference]
        public string ApiKey { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as IndexerPreferences);
        }

        public bool Equals(IndexerPreferences other)
        {
            return other != null &&
                   Enabled == other.Enabled &&
                   UseTls == other.UseTls &&
                   Host == other.Host &&
                   Resource == other.Resource &&
                   ApiKey == other.ApiKey;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Enabled, UseTls, Host, Resource, ApiKey);
        }

        public static bool operator ==(IndexerPreferences preferences1, IndexerPreferences preferences2)
        {
            return EqualityComparer<IndexerPreferences>.Default.Equals(preferences1, preferences2);
        }

        public static bool operator !=(IndexerPreferences preferences1, IndexerPreferences preferences2)
        {
            return !(preferences1 == preferences2);
        }
    }
}
