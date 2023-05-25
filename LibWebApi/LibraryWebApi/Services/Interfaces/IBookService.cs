using LibraryWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookService
{
    Task<Book> CreateBookAsync(Book book);
    Task<Book> UpdateBookAsync(int bookId, Book book);
    Task<bool> DeleteBookAsync(int bookId);
    Task<Book> GetBookByIdAsync(int bookId);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<IEnumerable<Book>> FilterBooksAsync(BookFilter filter);
    Task<IEnumerable<Book>> SortBooksAsync(string sortBy);
    Task<BookResult> GetBooksByPageAsync(int page, BookFilter filter);
    Task<BookResult> GetBooksByPageAsync(int page, string title);
}