using LibraryWebApi.Models.domain;
using LibraryWebApi.Models.http;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;

namespace LibraryWebApi.Services;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }
    
    public Task<int> AddBookAsync(BookRequest bookRequest)
    {   
        var book = new Book
        {
            Title = bookRequest.Title,
            Author = bookRequest.Author,
            PublicationYear = bookRequest.PublicationYear
        };
        
        return bookRepository.AddBookAsync(book);
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return bookRepository.GetAllBooksAsync();
    }

    public async Task<bool> UpdateBookAsync(int bookId, BookRequest bookRequest)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId);
        
        if (book == null)
        {
            return false;
        }

        book.Title = bookRequest.Title;
        book.Author = bookRequest.Author;
        book.PublicationYear = bookRequest.PublicationYear;

        var affectedRows = await bookRepository.UpdateBookAsync(book);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteBookByIdAsync(int bookId)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId);
        
        if (book == null)
        {
            return false;
        }
        
        var affectedRows = await bookRepository.DeleteBookByIdAsync(bookId);
        return affectedRows > 0;
    }
    
    public Task<Book> GetBookById(int bookId)
    {
        return bookRepository.GetBookByIdAsync(bookId);
    }
}
