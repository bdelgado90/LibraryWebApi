using System.Diagnostics.CodeAnalysis;

namespace LibraryWebApi.Models.http;

[ExcludeFromCodeCoverage]
public class UserRequest
{
    public string Username { get; set; } 
    public string Password { get; set; } 
}
