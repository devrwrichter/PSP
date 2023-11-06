using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Stone.PSP.Infra.Context
{
    public class PaymentDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        public PaymentDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlServer");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
