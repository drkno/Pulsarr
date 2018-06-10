namespace Pulsarr.Preferences.ServiceInterfaces
{
    public interface IPreferenceService
    {
        T Get<T>(string key, T defaultValue);
        void Set<T>(string key, T value);
    }
}
