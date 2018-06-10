using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulsarr.Metadata.Model;

namespace Pulsarr.Metadata.ServiceInterfaces
{
    public interface IMetaDataSource
    {
        Task<ImmutableList<Book>> Search(string title);
        Task<Book> Lookup(string dataSourceId, string bookId);
    }
}
