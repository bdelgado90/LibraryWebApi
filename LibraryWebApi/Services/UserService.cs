using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryWebApi.Models.domain;
using LibraryWebApi.Models.http;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LibraryWebApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
    }

    public async Task<int> RegisterUserAsync(UserRequest userRequest)
    {
        var existingUser = await userRepository.GetUserByUsernameAsync(userRequest.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already taken.");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
        var user = new User
        {
            Username = userRequest.Username,
            PasswordHash = hashedPassword
        };
        return await userRepository.CreateUserAsync(user);
    }

    public async Task<string?> LoginAsync(UserRequest userRequest)
    {
        var user = await userRepository.GetUserByUsernameAsync(userRequest.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userRequest.Password, user.PasswordHash))
        {
            return null;
        }
        
        return GenerateJwtToken(user);
    }

    #region Private Methods
    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("userId", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}
