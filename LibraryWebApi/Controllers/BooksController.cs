using LibraryWebApi.Models.http;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BookRequest bookRequest)
    {
        var bookId = await bookService.AddBookAsync(bookRequest);
        return Ok(new { BookId = bookId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await bookService.GetAllBooksAsync();
        return Ok(books);
    }
    
    [HttpGet("{bookId:int}")]
    public async Task<IActionResult> GetBookById(int bookId)
    {
        var book = await bookService.GetBookById(bookId);
        return Ok(book);
    }

    [HttpPut("{bookId:int}")]
    public async Task<IActionResult> UpdateBook(int bookId, [FromBody] BookRequest bookRequest)
    {
        var bookUpdated = await bookService.UpdateBookAsync(bookId, bookRequest);
        
        if (bookUpdated)
        {
            return Ok(new { Message = "Book updated successfully." });
        }
        
        return NotFound(new { Message = "Book Not Found." });
    }

    [HttpDelete("{bookId}")]
    public async Task<IActionResult> DeleteBook(int bookId)
    {
        var bookDeleted = await bookService.DeleteBookByIdAsync(bookId);
        
        if (bookDeleted)
        {
            return Ok(new { Message = "Book deleted successfully." });
        }
        
        return NotFound(new { Message = "Book Not Found." });
    }
}
