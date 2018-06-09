using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Pulsarr.Model.Metadata
{
    interface IDataSource
    {
        string SourceId { get; }
        Task<ImmutableList<IBook>> Search(string title);
        Task<IDetailedBook> Lookup(ulong id);
    }
}
