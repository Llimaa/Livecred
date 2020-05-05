using System;
using System.Data;
using System.Threading.Tasks;

namespace Livecred.Infra.Context.Core
{
    public interface IDB: IDisposable
    {
        Task<IDbConnection> GetConAsync();
    }
}
