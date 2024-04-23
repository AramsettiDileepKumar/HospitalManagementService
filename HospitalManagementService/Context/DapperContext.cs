using System.Data;
using System.Data.SqlClient;

namespace HospitalManagementService.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _config;

        public DapperContext(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection getConnection() => new SqlConnection(_config.GetConnectionString("SqlConnection"));
    }


}
