using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryWebApi.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAllBooks()
    {
        try
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }
        catch (Exception ex)
        {
            // Обработка ошибки
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        try
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            // Обработка ошибки
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        try
        {
            var createdBook = await _bookService.CreateBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }
        catch (Exception ex)
        {
            // Обработка ошибки
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
    {
        try
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var existingBook = await _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            if (!HasPermissionToEditBook(existingBook))
            {
                return Forbid();
            }

            var updatedBook = await _bookService.UpdateBook(book);
            return Ok(updatedBook);
        }
        catch (Exception ex)
        {
            // Обработка ошибки
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            if (!HasPermissionToDeleteBook(book))
            {
                return Forbid();
            }

            await _bookService.DeleteBook(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            // Обработка ошибки
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    private bool HasPermissionToEditBook(Book book)
    {
        // Проверка прав доступа для редактирования книги.
        // Верните true, если пользователь имеет право редактировать книгу, иначе false.
        // Например, можно проверить роль пользователя или другие правила доступа.

        // В данном примере разрешается редактирование всем пользователям
        return true;
    }

    private bool HasPermissionToDeleteBook(Book book)
    {
        // Проверка прав доступа для удаления книги.
        // Верните true, если пользователь имеет право удалить книгу, иначе false.
        // Например, можно проверить роль пользователя или другие правила доступа.

        // В данном примере разрешается удаление всем пользователям
        return true;
    }
}