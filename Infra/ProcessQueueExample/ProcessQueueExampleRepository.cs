using Dapper;
using Microsoft.Extensions.Configuration;
using Services.ProcessQueueExample;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ProcessQueueExample
{
    public class ProcessQueueExampleRepository : IProcessQueueExampleRepository
    {
        public IConfiguration _configuration;

        public ProcessQueueExampleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ConsultaAsync()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("")))
            {
                return await conn.QueryFirstAsync<string>("");
            }
        }
    }
}
