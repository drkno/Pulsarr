using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Goodreads;
using Pulsarr.Metadata.Model;
using Pulsarr.Metadata.ServiceInterfaces;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Metadata.Sources.GoodReads
{
    public class GoodReadsDataSource : IDataSource
    {
        private readonly IPreferenceService _preferenceService;
        public string SourceId { get; } = "GoodReads";
        public bool Enabled => _preferenceService.Get("goodreads.enabled", true);
        private readonly IGoodreadsClient _client;

        public GoodReadsDataSource(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
            _client = GoodreadsClient.Create(
                preferenceService.Get("goodreads.apikey", "ckvsiSDsuqh7omh74ZZ6Q"), // todo: this is the lazylibrarian key...
                preferenceService.Get("goodreads.apisecret", "")
            );
        }

        public async Task<ImmutableList<Book>> Search(string search)
        {
            var resultBooks = new List<Book>();
            var books = await _client.Books.Search(search);
            if (books.Pagination.TotalItems == 0)
            {
                return resultBooks.ToImmutableList();
            }
            foreach (var book in books.List)
            {
                var junkTitle = book.BestBook?.Title ?? book.OriginalTitle;
                (string title, string series, float? seriesPart) = Utils.ExtractTitleData(junkTitle);
                var impl = new Book(
                    SourceId,
                    (book.BestBook?.Id ?? book.BestBookId ?? book.Id).ToString(),
                    title,
                    "",
                    book.BestBook?.AuthorName,
                    book.BestBook?.ImageUrl,
                    series,
                    seriesPart,
                    book.OriginalPublicationDate
                );
                resultBooks.Add(impl);
            }
            return resultBooks.ToImmutableList();
        }

        public async Task<Book> Lookup(string id)
        {
            var book = await _client.Books.GetByBookId(long.Parse(id));
            (string title, string series, float? seriesPart) = Utils.ExtractTitleData(book.Title);
            return new Book(
                SourceId,
                id,
                title,
                book.Description,
                string.Join(',', book.Authors.Select(s => s.Name)),
                book.ImageUrl,
                series,
                seriesPart,
                book.PublicationDate
            );
        }
    }
}
