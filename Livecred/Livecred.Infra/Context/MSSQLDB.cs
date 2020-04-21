using Livecred.Infra.Context.Core;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Livecred.Infra.Context
{
    public class MSSQLDB : IDB
    {
        SqlConnection DB;
        private readonly string _con;

        public MSSQLDB(IDbConfiguration config)
        {
            _con = config.StringConnection;
        }
        public void Dispose()
        {
            if (DB != null)
            {
                if (DB.State == ConnectionState.Open)
                    DB.Close();
                DB.Dispose();
            }
        }

        public async Task<IDbConnection> GetConAsync()
        {
            return await Task.Run(() => { return DB = new SqlConnection(_con); });
        }
    }
}
