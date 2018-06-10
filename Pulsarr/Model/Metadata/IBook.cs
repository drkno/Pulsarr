namespace Pulsarr.Model.Metadata
{
    public interface IBook
    {
        string SourceId { get; }
        ulong SourceBookId { get; }
        string Title { get; }
        string Author { get; }
        string ImageUrl { get; }
        string Series { get; }
        float SeriesPart { get; }
    }
}
