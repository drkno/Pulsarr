using Goodreads.Models.Response;
using Newtonsoft.Json;

namespace Pulsarr.Model.Metadata.GoodReads
{
    public class GoodReadsBookSummary : IBook
    {
        [JsonIgnore]
        public Work SourceData { get; }
        public string SourceId { get; }
        public ulong SourceBookId => (ulong) (SourceData.BestBook?.Id ?? SourceData.BestBookId);
        public string Title { get; }
        public string Author => SourceData.BestBook?.AuthorName;
        public string ImageUrl => SourceData.BestBook?.ImageUrl;
        public string Series { get; }
        public float SeriesPart { get; }

        public GoodReadsBookSummary(string sourceId, Work sourceData)
        {
            SourceId = sourceId;
            SourceData = sourceData;
            var title = SourceData.BestBook?.Title ?? SourceData.OriginalTitle;
            (Title, Series, SeriesPart) = Utils.ExtractTitleData(title);
        }
    }
}
