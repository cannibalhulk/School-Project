using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryWebApi.Controllers;


[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    // POST api/books
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Book>> CreateBookAsync(Book book)
    {
        var createdBook = await _bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBookByIdAsync), new { id = createdBook.Id }, createdBook);
    } 

    [HttpPut("{bookId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBookAsync(int bookId, Book book)
    {

        var updatedBook = await _bookService.UpdateBookAsync(bookId, book);
        if (updatedBook == null)
        {
            return NotFound();
        }

        return Ok();
    } 

    [HttpDelete("{bookId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBookAsync(int bookId)
    {
        var result = await _bookService.DeleteBookAsync(bookId);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpGet("{bookId}")]
    public async Task<ActionResult<Book>> GetBookByIdAsync(int bookId)
    {
        var book = await _bookService.GetBookByIdAsync(bookId);
        if (book == null)
        {
            return NotFound();
        }
        return book;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<Book>>> FilterBooksAsync(BookFilter filter)
    {
        var filteredBooks = await _bookService.FilterBooksAsync(filter);
        return Ok(filteredBooks);
    }

    [HttpGet("sort")]
    public async Task<ActionResult<IEnumerable<Book>>> SortBooksAsync(string sortBy)
    {
        var sortedBooks = await _bookService.SortBooksAsync(sortBy);
        return Ok(sortedBooks);
    } 

    [HttpGet("page/{page}")]
    public async Task<ActionResult<BookResult>> GetBooksByPageAsync(int page, BookFilter filter)
    {
        var bookResult = await _bookService.GetBooksByPageAsync(page, filter);
        return Ok(bookResult);
    }
}
 