using Livecred.Infra.Context.Core;
using Microsoft.Extensions.Configuration;
namespace Livecred.Configurations
{
    public class DbConfiguration : IDbConfiguration
    {
        private readonly IConfiguration _dbConfiguration;

        public DbConfiguration(IConfiguration configuration)
        {
            _dbConfiguration = configuration;
        }

        public string StringConnection => _dbConfiguration.GetConnectionString("DefaultConnection");
    }
}
