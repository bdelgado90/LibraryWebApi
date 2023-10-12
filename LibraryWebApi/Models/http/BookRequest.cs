using System.Diagnostics.CodeAnalysis;

namespace LibraryWebApi.Models.http;

[ExcludeFromCodeCoverage]
public class BookRequest
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
}
