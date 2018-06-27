using System;

namespace Pulsarr.Importers
{
    public interface IImporter
    {
        Task Import(string type, string id);
    }
}
