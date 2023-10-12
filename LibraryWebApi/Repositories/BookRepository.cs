using LibraryWebApi.Models.domain;
using LibraryWebApi.Repositories.Interfaces;

namespace LibraryWebApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDapperWrapper dapperWrapper;

    public BookRepository(IDapperWrapper dapperWrapper)
    {
        this.dapperWrapper = dapperWrapper;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        const string sql = @"
            SELECT BookId,
                Title,
                Author,
                PublicationYear
            FROM Books
            ORDER BY PublicationYear,
                Title "; 
        var books =  await dapperWrapper.QueryAsync<Book>(sql);
        return books;
    }

    public async Task<int> AddBookAsync(Book book)
    {
        const string sql = @"
            INSERT INTO Books (Title, Author, PublicationYear)
            VALUES (@Title, @Author, @PublicationYear);
            SELECT CAST(SCOPE_IDENTITY() as int)"; 
        var bookId =  await dapperWrapper.QuerySingleAsync<int>(sql, book);
        return bookId;
    }

    public async Task<int> UpdateBookAsync(Book book)
    {
        const string sql = @"
            UPDATE Books 
            SET Title = @Title, 
                Author = @Author, 
                PublicationYear = @PublicationYear 
            WHERE BookId = @BookId";
        
        var affectedRows = await dapperWrapper.ExecuteAsync(sql, book);
        return affectedRows;
    }

    public async Task<Book> GetBookByIdAsync(int bookId)
    {
        const string sql = @"
            SELECT BookId, Title, Author, PublicationYear
            FROM Books 
            WHERE BookId = @Id";

        var book = await dapperWrapper.QuerySingleOrDefaultAsync<Book>(sql, new { Id = bookId } );
        return book;
    }

    public async Task<int> DeleteBookByIdAsync(int bookId)
    {
        const string sql = @"
            DELETE FROM Books 
            WHERE BookId = @Id";
        
        var affectedRows = await dapperWrapper.ExecuteAsync(sql, new { Id = bookId });
        return affectedRows;
    }
}
