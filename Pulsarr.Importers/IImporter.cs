using System;
using System.Threading.Tasks;

namespace Pulsarr.Importers
{
    public interface IImporter
    {
        Task Import(string type, string id);
    }
}
