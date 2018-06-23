using System.Collections.Generic;

namespace Pulsarr.Preferences.ServiceInterfaces
{
    public interface IPreferenceService
    {
        IReadOnlyDictionary<string, string> AllEntries { get; }
        T Get<T>(string key, T defaultValue, bool throwOnNotFound = false);
        void Set<T>(string key, T value);
        T[] GetArray<T>(string key);
        void SetArray<T>(string key, T[] value);
        T GetObject<T>(string key, bool throwOnNotFound = false);
        void SetObject<T>(string key, T value);
        T[] GetObjectArray<T>(string key);
        void SetObjectArray<T>(string key, T[] value);
    }
}
