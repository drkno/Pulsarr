using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Pulsarr.Preferences.DataModel.Preferences
{
    public class Preference : Annotation
    {
        public Preference(string name, object value) : base(name, value)
        {
        }
    }
}
