using System.Collections.Generic;
using System.ComponentModel;
using Pulsarr.Preferences.Exceptions;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.Stores
{
    public abstract class BasePreferenceService : IPreferenceService
    {
        public abstract IEnumerable<string> Keys { get; }
        public abstract IReadOnlyDictionary<string, string> AllEntries { get; }
        protected abstract string this[string key] { get; set; }

        public T Get<T>(string key, T defaultValue)
        {
            try
            {
                var value = this[key];
                return (T) TypeDescriptor.GetConverter(defaultValue.GetType()).ConvertFromString(value);
            }
            catch (NoSuchPreferenceException)
            {
                Set(key, defaultValue);
            }
            return defaultValue;
        }

        public void Set<T>(string key, T value)
        {
            this[key] = value.ToString();
        }
    }
}
