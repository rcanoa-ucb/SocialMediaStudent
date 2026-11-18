using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SocialMedia.Core.Interfaces;
using System.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;
        private readonly string _mySqlConn;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
            _mySqlConn = _config.GetConnectionString("ConnectionMySql") ?? string.Empty;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_mySqlConn);
        }
    }
}
