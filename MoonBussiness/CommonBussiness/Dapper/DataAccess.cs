using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace MoonBussiness.CommonBussiness.Dapper
{
    public class DataAccess : IDataAcess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string query, P parameters, string connectionId = "ApiCodeFirst")
        {
            string connectionString = _config.GetConnectionString(connectionId);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            return await connection.QueryAsync<T>(query, parameters);
        }

        public async Task SaveData<P>(string query, P parameters, string connectionId = "ApiCodeFirst")
        {
            string connectionString = _config.GetConnectionString(connectionId);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
