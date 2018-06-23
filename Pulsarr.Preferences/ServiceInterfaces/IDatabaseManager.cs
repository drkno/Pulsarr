using System;
using System.Threading.Tasks;
using Pulsarr.Preferences.DataModel;

namespace Pulsarr.Preferences.ServiceInterfaces
{
    public interface IDatabaseManager
    {
        T PerformDbTask<T>(Func<DatabaseStore, T> dbUser);
        Task<T> PerformDbTaskAsync<T>(Func<DatabaseStore, Task<T>> dbUser);
    }
}
