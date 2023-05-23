using LibraryWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<IEnumerable<Book>> GetSortedBooksAsync(string sortBy);
    Task<IEnumerable<Book>> GetFilteredBooksAsync(BookFilter filter);
    Task<BookResult> GetBooksByPageAsync(int page, BookFilter filter);
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}