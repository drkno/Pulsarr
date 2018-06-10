using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Goodreads;

namespace Pulsarr.Model.Metadata.GoodReads
{
    public class GoodReadsDataSource : IDataSource
    {
        private const string API_KEY = "ckvsiSDsuqh7omh74ZZ6Q";
        private const string API_SECRET = "";

        private static readonly IGoodreadsClient client = GoodreadsClient.Create(API_KEY, API_SECRET);

        public string SourceId { get; } = "GoodReads";

        public async Task<ImmutableList<IBook>> Search(string title)
        {
            try
            {
                var books = await client.Books.Search(title);
                return books.List.Select(b => (IBook) new GoodReadsBookSummary(SourceId, b)).ToImmutableList();
            }
            catch (Exception)
            {
                return new List<IBook>().ToImmutableList();
            }
        }

        public async Task<IDetailedBook> Lookup(ulong id)
        {
            try
            {
                var book = await client.Books.GetByBookId((long) id);
                return new GoodReadsBookDetail(SourceId, book);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
