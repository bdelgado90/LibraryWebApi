using System.Diagnostics.CodeAnalysis;

namespace LibraryWebApi.Models.domain;

[ExcludeFromCodeCoverage]
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}
