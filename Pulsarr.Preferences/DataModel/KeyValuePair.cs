using System.ComponentModel.DataAnnotations;

namespace Pulsarr.Preferences.DataModel
{
    public class KeyValuePair
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValuePair() {}

        public KeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
