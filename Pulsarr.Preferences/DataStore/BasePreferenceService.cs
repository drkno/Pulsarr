using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Pulsarr.Preferences.DataModel.Preferences;
using Pulsarr.Preferences.Exceptions;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.DataStore
{
    public abstract class BasePreferenceService : IPreferenceService
    {
        public abstract IReadOnlyDictionary<string, string> AllEntries { get; }
        protected abstract string this[string key] { get; set; }
        protected abstract void DeleteKeysStartingWith(string key);
        
        public T Get<T>(string key, T defaultValue, bool throwOnNotFound = false)
        {
            try
            {
                var value = this[key];
                return (T) TypeDescriptor.GetConverter(defaultValue.GetType()).ConvertFromString(value);
            }
            catch (NoSuchPreferenceException)
            {
                if (throwOnNotFound)
                {
                    throw;
                }
                Set(key, defaultValue);
            }
            return defaultValue;
        }

        public void Set<T>(string key, T value)
        {
            this[key] = value?.ToString();
        }

        public T[] GetArray<T>(string key)
        {
            var items = new List<T>();
            try
            {
                var i = 0;
                while (true)
                {
                    items.Add(Get($"{key}.{i++}", default(T), true));
                }
            }
            catch (NoSuchPreferenceException)
            {
            }
            return items.ToArray();
        }

        public void SetArray<T>(string key, T[] value)
        {
            DeleteKeysStartingWith(key);
            for (var i = 0; i < value.Length; i++)
            {
                Set($"{key}.{i}", value[i]);
            }
        }

        public T GetObject<T>(string key, bool throwOnNotFound = false)
        {
            var properties = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(Preference), false));
            var instance = Activator.CreateInstance<T>();
            foreach (var property in properties)
            {
                var value = this[$"{key}.{property.Name}"];
                property.SetValue(instance, TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(value));
            }
            return instance;
        }

        public void SetObject<T>(string key, T value)
        {
            DeleteKeysStartingWith(key);
            var properties = typeof(T).GetProperties()
                .Where(prop => prop.IsDefined(typeof(Preference), false));
            foreach (var property in properties)
            {
                Set($"{key}.{property.Name}", property.GetValue(value));
            }
        }

        public T[] GetObjectArray<T>(string key)
        {
            var items = new List<T>();
            try
            {
                var i = 0;
                while (true)
                {
                    items.Add(GetObject<T>($"{key}.{i++}", true));
                }
            }
            catch (NoSuchPreferenceException)
            {
            }
            return items.ToArray();
        }

        public void SetObjectArray<T>(string key, T[] value)
        {
            DeleteKeysStartingWith(key);
            for (var i = 0; i < value.Length; i++)
            {
                SetObject($"{key}.{i}", value[i]);
            }
        }
    }
}
