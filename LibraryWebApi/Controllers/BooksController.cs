using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryWebApi.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    // GET api/books/sort/{sortBy}
    [HttpGet("sort/{sortBy}")]
    public async Task<ActionResult<IEnumerable<Book>>> GetSortedBooksAsync(string sortBy)
    {
        var books = await _bookService.GetSortedBooksAsync(sortBy);
        return Ok(books);
    }

    // GET api/books/filter?title={title}&author={author}&...
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Book>>> GetFilteredBooksAsync([FromQuery] BookFilter filter)
    {
        var books = await _bookService.GetFilteredBooksAsync(filter);
        return Ok(books);
    }

    // GET api/books/page/{page}?title={title}&author={author}&...
    [HttpGet("page/{page}")]
    public async Task<ActionResult<BookResult>> GetBooksByPageAsync(int page, [FromQuery] BookFilter filter)
    {
        var result = await _bookService.GetBooksByPageAsync(page, filter);
        return Ok(result);
    }

    // POST api/books
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Book>> CreateBookAsync(Book book)
    {
        var createdBook = await _bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBookByIdAsync), new { id = createdBook.Id }, createdBook);
    }

    // GET api/books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    // PUT api/books/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBookAsync(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        var updatedBook = await _bookService.UpdateBookAsync(id, book);
        if (updatedBook == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE api/books/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        var deletedBook = await _bookService.DeleteBookAsync(id);
        if (deletedBook == null!)
        {
            return NotFound();
        }

        return NoContent();
    }
}
