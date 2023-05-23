using Bogus;
using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace LibraryWebApi.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _dbContext;

    public BookService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetSortedBooksAsync(string sortBy)
    {
        var property = typeof(Book).GetProperty(sortBy);
        var expression = Expression.Lambda<Func<Book, object>>(
            Expression.Convert(Expression.Property(null, property!), typeof(object)),
            Expression.Parameter(typeof(Book))
        );

        return await _dbContext.Books.OrderBy(expression).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetFilteredBooksAsync(BookFilter filter)
    {
        var query = _dbContext.Books.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(b => b.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrEmpty(filter.Author))
        {
            query = query.Where(b => b.Author.Contains(filter.Author));
        }

        // Добавьте дополнительные условия фильтрации по другим полям модели Book

        return await query.ToListAsync();
    }

    public async Task<BookResult> GetBooksByPageAsync(int page, BookFilter filter)
    {
        const int pageSize = 20;
        var query = _dbContext.Books.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(b => b.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrEmpty(filter.Author))
        {
            query = query.Where(b => b.Author.Contains(filter.Author));
        }

        // Добавьте дополнительные условия фильтрации по другим полям модели Book

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        var books = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new BookResult
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            Books = books
        };
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _dbContext.Books.FindAsync(id)!;
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBookAsync(int id, Book book)
    {
        var existingBook = await _dbContext.Books.FindAsync(id);

        if (existingBook == null)
        {
            return null!;
        }

        // Обновление свойств существующей книги
        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Description = book.Description;
        // Обновите остальные свойства

        await _dbContext.SaveChangesAsync();

        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);

        if (book == null)
        {
            return false;
        }

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
