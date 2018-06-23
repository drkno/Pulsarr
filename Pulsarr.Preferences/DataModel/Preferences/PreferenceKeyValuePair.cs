using System.ComponentModel.DataAnnotations.Schema;

namespace Pulsarr.Preferences.DataModel.Preferences
{
    [Table("Preferences")]
    public class PreferenceKeyValuePair : KeyValuePair
    {
        public PreferenceKeyValuePair(): base() {}
        public PreferenceKeyValuePair(string key, string value) : base(key, value)
        {
        }
    }
}
