using System.Data;
using Dapper;
using LibraryWebApi.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LibraryWebApi.Repositories;

public class DapperWrapper(string connectionString) : IDapperWrapper
{
    private readonly IDbConnection connection = new SqlConnection(connectionString);

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        return connection.QuerySingleAsync<T>(sql, param);
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        return connection.QueryAsync<T>(sql, param);
    }

    public Task<int> ExecuteAsync(string sql, object? param = null)
    {
        return connection.ExecuteAsync(sql, param);
    }

    public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null)
    {
        return connection.QuerySingleOrDefaultAsync<T>(sql, param);
    }
}
