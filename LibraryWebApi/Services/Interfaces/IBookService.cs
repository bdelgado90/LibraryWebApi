using LibraryWebApi.Models.domain;
using LibraryWebApi.Models.http;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookService
{
    Task<int> AddBookAsync(BookRequest bookRequest);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<bool> UpdateBookAsync(int bookId, BookRequest bookRequest);
    Task<bool> DeleteBookByIdAsync(int bookId);
    Task<Book> GetBookById(int bookId);
}
