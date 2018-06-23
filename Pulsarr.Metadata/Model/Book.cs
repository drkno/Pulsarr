using System;

namespace Pulsarr.Metadata.Model
{
    public class Book
    {
        public Book(string dataSourceId, string sourceBookId, string title, string description, string author,
            string imageUrl, string series, float? seriesPart, DateTime? publicationDate)
        {
            DataSourceId = dataSourceId;
            SourceBookId = sourceBookId;
            Title = title;
            Description = description;
            Author = author;
            ImageUrl = imageUrl;
            Series = series;
            SeriesPart = seriesPart;
            PublicationDate = publicationDate;
        }

        public string DataSourceId { get; }
        public string SourceBookId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Author { get; }
        public string ImageUrl { get; }
        public string Series { get; }
        public float? SeriesPart { get; }
        public DateTime? PublicationDate { get; }
    }
}
