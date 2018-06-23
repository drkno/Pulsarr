using System.ComponentModel.DataAnnotations.Schema;

namespace Pulsarr.Preferences.DataModel.Metadata
{
    [Table("DatabaseMetadata")]
    public class MetaDataKeyValuePair : KeyValuePair
    {
        public MetaDataKeyValuePair(): base() {}
        public MetaDataKeyValuePair(string key, string value) : base(key, value)
        {
        }
    }
}
