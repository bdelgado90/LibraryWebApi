using System.Diagnostics.CodeAnalysis;

namespace LibraryWebApi.Models.domain;

[ExcludeFromCodeCoverage]
public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
}
