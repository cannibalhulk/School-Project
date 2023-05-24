using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Errors.Model;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryWebApi.Services;

public class UserService : IUserService
{
    private readonly LibraryDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private IConfiguration _configuration;

    public UserService(LibraryDbContext context, IPasswordHasher passwordHasher, IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;

        if (!_context.Users.Any(u => u.Role == UserRole.Admin.ToString()))
        {
            var user = new User
            {
                FirstName = _configuration["AdminsData:FirstName"]!,
                LastName = _configuration["AdminsData:LastName"]!,
                DateOfBirth = new DateTime(2030, 7, 20),
                PhoneNumber = _configuration["AdminsData:PhoneNumber"]!,
                Email = _configuration["AdminsData:Email"]!,
                PersonalCode = _configuration["AdminsData:FinancialCode"]!,
                Password = _configuration["AdminsData:Password"]!,
                Role = UserRole.Admin.ToString()
            };

            user.FavoriteBooks = new List<FavoriteBook>();
            user.Password = _passwordHasher.HashPassword(user.Password);
            user.PersonalCode = _passwordHasher.HashPassword(user.PersonalCode);
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public User Authenticate(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);

        if (user == null)
        {
            return null!;
        }

        if (!_passwordHasher.VerifyPassword(user.Password, password))
        {
            return null!;
        }

        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.Role = UserRole.Normal.ToString();
        user.FavoriteBooks = new List<FavoriteBook>();
        user.Password = _passwordHasher.HashPassword(user.Password);
        user.PersonalCode = _passwordHasher.HashPassword(user.PersonalCode);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(int userId, JsonPatchDocument<User> user)
    {
        //        var existingUser = await _context.Users.FindAsync(userId);
        //        if (existingUser == null)
        //        {
        //            throw new NotFoundException("User not found");
        //        }

        //        existingUser.FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : existingUser.FirstName;
        //        existingUser.LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : existingUser.FirstName;
        //        existingUser.PhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : existingUser.PhoneNumber;
        //        existingUser.Email = !string.IsNullOrEmpty(user.Email) ? user.Email : existingUser.Email;
        //        existingUser.Role = !string.IsNullOrEmpty(user.Role) ? user.Role : existingUser.Role;
        //#pragma warning disable CS8073 
        //        existingUser.DateOfBirth = user.DateOfBirth != null ? user.DateOfBirth : existingUser.DateOfBirth;
        //#pragma warning restore CS8073 
        //       // _context.Users.Update(existingUser);

        //        await _context.SaveChangesAsync();
        //        return existingUser;

        var res = await _context.Users.FindAsync(userId);
        if (res == null)
        {
            throw new NotFoundException("User not found");
        }
        user.ApplyTo(res);

        await _context.SaveChangesAsync();

        return await _context.Users.FindAsync(userId);
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = GetUserByIdAsync(userId).Result; 

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<IEnumerable<User>> FilterUsersAsync(User filter)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            query = query.Where(u => u.FirstName.Contains(filter.FirstName));
        }
#pragma warning disable CS8073 
        if (filter.DateOfBirth != null)
        {
            query = query.Where(u => u.DateOfBirth == filter.DateOfBirth);
        }
#pragma warning restore CS8073 
        if (!string.IsNullOrEmpty(filter.LastName))
        {
            query = query.Where(u => u.LastName == filter.LastName);
        }
        if (!string.IsNullOrEmpty(filter.Email))
        {
            query = query.Where(u => u.Email == filter.Email);
        }
        if (!string.IsNullOrEmpty(filter.PhoneNumber))
        {
            query = query.Where(u => u.PhoneNumber == filter.PhoneNumber);
        }
        if (!string.IsNullOrEmpty(filter.PersonalCode))
        {
            query = query.Where(u => u.PersonalCode == filter.PersonalCode);
        }
        if (!string.IsNullOrEmpty(filter.Role.ToString()))
        {
            query = query.Where(u => u.Role == filter.Role);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<User>> SortUsersAsync(string sortBy)
    {

        var property = typeof(User).GetProperty(sortBy);
        var expression = Expression.Lambda<Func<User, object>>(
            Expression.Convert(Expression.Property(null, property!), typeof(object)),
            Expression.Parameter(typeof(User))
        );

        return await _context.Users.OrderBy(expression).ToListAsync();
    }
    public async Task<bool> AddBookToFavoritesAsync(int userId, int bookId)
    {
        var user = await _context.Users.Include(u => u.FavoriteBooks).FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException("Book not found");
        }

        user.FavoriteBooks.Add(new FavoriteBook
        {
            BookId= book.Id,
            Book = book,
            User = user,
            UserId= userId,
        });

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RemoveFromFavoritesAsync(int userId, int bookId)
    {
        var user = await _context.Users.Include(u => u.FavoriteBooks).FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException("Book not found");
        }

        var list = _context.FavoriteBooks.Where(fb => fb.UserId == userId && fb.BookId == bookId).ToList<FavoriteBook>();
        _context.FavoriteBooks.RemoveRange(list);
        await _context.SaveChangesAsync();
        return true;
    }


    //public async Task<bool> AddBookRatingAsync(int userId, int bookId, int rating)
    //{
    //    // Логика добавления рейтинга книги пользов
    //    return false;
    //}
    //public async Task<bool> RemoveBookRatingAsync(int userId, int bookId)
    //{
    //    var user = await _context.Users.Include(u => u.FavoriteBooks).FirstOrDefaultAsync(u => u.Id == userId);
    //    if (user == null)
    //    {
    //        throw new NotFoundException("User not found");
    //    }

    //    var book = await _context.Books.FindAsync(bookId);
    //    if (book == null)
    //    {
    //        throw new NotFoundException("Book not found");
    //    }

    //    var list = _context.FavoriteBooks.Where(fb => fb.UserId == userId && fb.BookId == bookId).ToList<FavoriteBook>();
    //    _context.FavoriteBooks.RemoveRange(list);
    //    await _context.SaveChangesAsync();
    //    return true;
    //}

}
