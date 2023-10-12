using LibraryWebApi.Models.domain;

namespace LibraryWebApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<int> CreateUserAsync(User user);
    Task<User> GetUserByUsernameAsync(string username);
}
