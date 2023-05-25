using Bogus;
using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<Book> CreateBookAsync(Book book)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBookAsync(int bookId, Book book)
    {
        var existingBook = await _dbContext.Books.FindAsync(bookId);
        if (existingBook == null)
        {
            return null!;
        }

        existingBook.Title = !string.IsNullOrEmpty(book.Title) ? book.Title : existingBook.Title;
        existingBook.Description = !string.IsNullOrEmpty(book.Description) ? book.Description : existingBook.Description;
        existingBook.Author = !string.IsNullOrEmpty(book.Author) ? book.Author : existingBook.Author;
#pragma warning disable CS8073 
        existingBook.CreationDate = book.CreationDate != null ? book.CreationDate : existingBook.CreationDate;
        existingBook.AddedDate = book.AddedDate != null ? book.AddedDate : existingBook.AddedDate;
#pragma warning restore CS8073 

        existingBook.PrivateStatus = book.PrivateStatus;
        existingBook.Genre = !string.IsNullOrEmpty(book.Genre) ? book.Genre : existingBook.Genre;
        existingBook.FileData = book.FileData != null ? book.FileData : existingBook.FileData;
        existingBook.PhotoData = book.PhotoData != null ? book.PhotoData : existingBook.PhotoData;
        //existingBook.Hashtags = book.Hashtags;

        await _dbContext.SaveChangesAsync();
        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        var book = await _dbContext.Books.FindAsync(bookId);
        if (book == null)
        {
            return false;
        }

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Book> GetBookByIdAsync(int bookId)
    {
#pragma warning disable CS8603 
        return await _dbContext.Books.FindAsync(bookId);
#pragma warning restore CS8603 
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task<IEnumerable<Book>> FilterBooksAsync(BookFilter filter)
    {
        var query = _dbContext.Books.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(b => b.Title.Contains(filter.Title));
        }
#pragma warning disable CS8073
        if (filter.CreatedDate != null)
        {
            query = query.Where(b => b.CreationDate == filter.CreatedDate);
        }
        if (filter.AddedDate != null)
        {
            query = query.Where(b => b.AddedDate == filter.AddedDate);
        }
#pragma warning restore CS8073 
        if (!string.IsNullOrEmpty(filter.Author))
        {
            query = query.Where(b => b.Author == filter.Author);
        }
        if (!string.IsNullOrEmpty(filter.Genre))
        {
            query = query.Where(b => b.Genre == filter.Genre);
        }
       // add hastags!!!!!!!!!!

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Book>> SortBooksAsync(string sortBy)
    {
    //    var query = _dbContext.Books.AsQueryable(); 
    //    switch (sortBy)
    //    {
    //        case "Title":
    //            query = query.OrderBy(b => b.Title);
    //            break;
    //        case "Description":
    //            query = query.OrderBy(b => b.Description);
    //            break;
    //        case "Author":
    //            query = query.OrderBy(b => b.Author);
    //            break;
    //        case "Genre":
    //            query = query.OrderBy(b => b.Genre);
    //            break;
    //        case "CreationDate":
    //            query = query.OrderBy(b => b.CreationDate);
    //            break;
    //        case "AddedDate":
    //            query = query.OrderBy(b => b.AddedDate);
    //            break;
    //        case "PrivateStatus":
    //            query = query.OrderBy(b => b.PrivateStatus);
    //            break;
    //    }

    //    return await query.ToListAsync();

        var property = typeof(Book).GetProperty(sortBy);
        var expression = Expression.Lambda<Func<Book, object>>(
            Expression.Convert(Expression.Property(null, property!), typeof(object)),
            Expression.Parameter(typeof(Book))
        );

        return await _dbContext.Books.OrderBy(expression).ToListAsync();
    }

    public async Task<BookResult> GetBooksByPageAsync(int page, BookFilter filter)
    {
        int pageSize = 20;
        var books = await FilterBooksAsync(filter);

        // Получаем общее количество книг
        int totalBooks = books.Count();

        // Вычисляем количество страниц и применяем пагинацию
        int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
        books = books.Skip((page - 1) * pageSize).Take(pageSize);

        return new BookResult
        {
            Books = books,
            TotalCount = totalBooks,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }
    public async Task<BookResult> GetBooksByPageAsync(int page, string title)
    {
        int pageSize = 20;
        var books = _dbContext.Books
            .Where(b => b.Title.Contains(title));

        // Получаем общее количество книг
        int totalBooks = books.Count();

        // Вычисляем количество страниц и применяем пагинацию
        int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
        books = books.Skip((page - 1) * pageSize).Take(pageSize);

        return new BookResult
        {
            Books = books,
            TotalCount = totalBooks,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }
     

}
