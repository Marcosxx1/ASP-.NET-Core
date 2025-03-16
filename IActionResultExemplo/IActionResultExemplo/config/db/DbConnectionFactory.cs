using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace IActionResultExemplo.config.db
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString); 
        }
    }
}
