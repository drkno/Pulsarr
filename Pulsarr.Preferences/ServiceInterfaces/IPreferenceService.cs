using System.Collections.Generic;

namespace Pulsarr.Preferences.ServiceInterfaces
{
    public interface IPreferenceService
    {
        IEnumerable<string> Keys { get; }
        IReadOnlyDictionary<string, string> AllEntries { get; }
        T Get<T>(string key, T defaultValue);
        void Set<T>(string key, T value);
    }
}
