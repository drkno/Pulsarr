using System;

namespace Pulsarr.Search.Manager.Model
{
    public interface SearchResult
    {
        string Title { get; }
        string Id { get; }
        string Link { get; }
        DateTime PublishDate { get; }
        string Category { get; }
        string Description { get; }
    }
}
