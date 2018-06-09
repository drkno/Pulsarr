using System;
using System.Linq;
using Goodreads.Models.Response;
using Newtonsoft.Json;

namespace Pulsarr.Model.Metadata.GoodReads
{
    public class GoodReadsBookDetail : IDetailedBook
    {
        [JsonIgnore]
        public Book SourceData { get; }

        public GoodReadsBookDetail(string sourceId, Book book)
        {
            SourceId = sourceId;
            SourceData = book;
            (Title, Series, SeriesPart) = Utils.ExtractTitleData(SourceData.Title);
        }

        public string SourceId { get; }
        public ulong SourceBookId => (ulong) SourceData.Id;
        public string Title { get; }
        public string Author => string.Join(',', SourceData.Authors.Select(s => s.Name));
        public string ImageUrl => SourceData.ImageUrl;
        public string Series { get; }
        public float SeriesPart { get; }
        public string Description => SourceData.Description;
        public DateTime? PublicationDate => SourceData.PublicationDate;
        public string Publisher => SourceData.Publisher;
        public float Rating => (float) SourceData.AverageRating;
    }
}
