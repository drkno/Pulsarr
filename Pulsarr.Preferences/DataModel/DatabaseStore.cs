using Microsoft.EntityFrameworkCore;
using Pulsarr.Preferences.DataModel.Library;
using Pulsarr.Preferences.DataModel.Metadata;
using Pulsarr.Preferences.DataModel.Preferences;

namespace Pulsarr.Preferences.DataModel
{
    public class DatabaseStore : DbContext
    {
        public DbSet<PreferenceKeyValuePair> Preferences { get; set; }
        public DbSet<LibraryItem> Library { get; set; }
        public DbSet<MetaDataKeyValuePair> DatabaseMetadata { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=preferences.db");
        }
    }
}
