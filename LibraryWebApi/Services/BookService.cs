using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _dbContext;

    public BookService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book> CreateBook(Book book)
    {
        _dbContext.Add(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<Book> GetBookById(int id)
    {
#pragma warning disable CS8603
        return await _dbContext.Set<Book>().FindAsync(id);
#pragma warning restore CS8603
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return await _dbContext.Set<Book>().ToListAsync();
    }

    public async Task<Book> UpdateBook(Book book)
    {
        _dbContext.Entry(book).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task DeleteBook(int id)
    {
        var book = await _dbContext.Set<Book>().FindAsync(id);
        if (book != null)
        {
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
