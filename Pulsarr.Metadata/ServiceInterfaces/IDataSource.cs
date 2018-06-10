using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulsarr.Metadata.Model;

namespace Pulsarr.Metadata.ServiceInterfaces
{
    public interface IDataSource
    {
        string SourceId { get; }
        Task<ImmutableList<Book>> Search(string title);
        Task<Book> Lookup(string id);
    }
}
