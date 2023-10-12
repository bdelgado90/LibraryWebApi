using LibraryWebApi.Models.domain;
using LibraryWebApi.Repositories.Interfaces;

namespace LibraryWebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDapperWrapper dapperWrapper;

    public UserRepository(IDapperWrapper dapperWrapper)
    {
        this.dapperWrapper = dapperWrapper;
    }
    
    public async Task<int> CreateUserAsync(User user)
    {
        const string sql = @"
            INSERT INTO Users (Username, PasswordHash)
            VALUES (@Username, @PasswordHash);
            SELECT CAST(SCOPE_IDENTITY() as int)";
        var userId = await dapperWrapper.QuerySingleAsync<int>(sql, user);
        return userId;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        const string sql = @"
            SELECT 
                UserId, 
                Username, 
                PasswordHash
            FROM Users
            WHERE Username = @Username";
        var user = await dapperWrapper.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
        return user;
    }
}
