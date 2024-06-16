namespace LibraryWebApi.Repositories.Interfaces;

public interface IDapperWrapper
{
    Task<T> QuerySingleAsync<T>(string sql, object? param = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    Task<int> ExecuteAsync(string sql, object? param = null);
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null);
}
