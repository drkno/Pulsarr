using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pulsarr.Preferences.DataModel.Preferences
{
    public class KeyValuePair
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
