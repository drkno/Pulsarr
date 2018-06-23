using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pulsarr.Preferences.DataModel.Library
{
    public class LibraryItem
    {
        public LibraryItem() {}

        public LibraryItem(string dataSourceId, string bookId)
        {
            DataSourceId = dataSourceId;
            SourceBookId = bookId;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public ulong Id { get; set; }
        [Required]
        public DateTime Added { get; set; } = DateTime.Now;
        [Required]
        public LibraryItemState State { get; set; } = LibraryItemState.Missing;
        public string LibraryLocation { get; set; }
        
        public string Description { get; set; }
        public string DataSourceId { get; set; }
        public string SourceBookId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public string Series { get; set; }
        public float? SeriesPart { get; set; }
        public DateTime? PublicationDate { get; set; }

        public static int Compare(LibraryItem x, LibraryItem y, string compareBy)
        {
            switch (compareBy)
            {
                case "id": return (int) (y.Id - x.Id);
                case "added": return DateTime.Compare(x.Added, y.Added);
                case "state": return y.State - x.State;
                case "libraryLocation": return string.CompareOrdinal(x.LibraryLocation, y.LibraryLocation);
                case "description": return string.CompareOrdinal(x.Description, y.Description);
                case "dataSourceId": return string.CompareOrdinal(x.DataSourceId, y.DataSourceId);
                case "sourceBookId": return string.CompareOrdinal(x.SourceBookId, y.SourceBookId);
                case "title": return string.CompareOrdinal(x.Title, y.Title);
                case "author": return string.CompareOrdinal(x.Author, y.Author);
                case "imageUrl": return string.CompareOrdinal(x.ImageUrl, y.ImageUrl);
                case "series": return string.CompareOrdinal(x.Series, y.Series);
                case "seriesPart": return (int) Math.Round((y.SeriesPart ?? 0f) - (x.SeriesPart ?? 0f));
                case "publicationDate": return DateTime.Compare(x.PublicationDate ?? DateTime.Now, y.PublicationDate ?? DateTime.Now);
            }
            return 0;
        }
    }
}
