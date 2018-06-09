using System;

namespace Pulsarr.Model.Metadata
{
    public interface IDetailedBook : IBook
    {
        string Description { get; }
        DateTime? PublicationDate { get; }
        string Publisher { get; }
        float Rating { get; }
    }
}
