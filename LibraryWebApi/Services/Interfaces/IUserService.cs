using LibraryWebApi.Models.http;

namespace LibraryWebApi.Services.Interfaces;

public interface IUserService
{
    Task<int> RegisterUserAsync(UserRequest userRequest);
    Task<string?> LoginAsync(UserRequest userRequest);
}
