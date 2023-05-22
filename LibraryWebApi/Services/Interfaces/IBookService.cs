using LibraryWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookService
{
    Task<Book> CreateBook(Book book);

    Task<Book> GetBookById(int id);

    Task<List<Book>> GetAllBooks();
    Task<Book> UpdateBook(Book book);

    Task DeleteBook(int id);
}