using LibraryWebApi.Models.domain;

namespace LibraryWebApi.Repositories.Interfaces;

public interface IBookRepository
{
    Task<int> AddBookAsync(Book book);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<int> UpdateBookAsync(Book book);
    Task<Book> GetBookByIdAsync(int bookId);
    Task<int> DeleteBookByIdAsync(int bookId);
}
