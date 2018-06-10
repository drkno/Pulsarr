using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Pulsarr.Model.Metadata.GoodReads;

namespace Pulsarr.Model.Metadata
{
    public class DataSourceCollector : IDataSource
    {
        public static DataSourceCollector Instance { get; } = new DataSourceCollector();

        private static readonly IDataSource[] _sources =
        {
            new GoodReadsDataSource()
        };

        private DataSourceCollector() {}

        public string SourceId { get; } = "Meta";
        public Task<ImmutableList<IBook>> Search(string title)
        {
            return _sources.First().Search(title);
        }

        public Task<IDetailedBook> Lookup(ulong id)
        {
            return _sources.First().Lookup(id);
        }
    }
}
