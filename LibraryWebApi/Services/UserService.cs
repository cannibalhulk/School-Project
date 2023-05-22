using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        if (!_context.Users.Any(u => u.Role == "Admin"))
        {
            var user = new User
            {
                FirstName = _configuration["AdminsData:FirstName"]!,
                LastName = _configuration["AdminsData:LastName"]!,
                DateOfBirth = new DateTime(2030,7,20),
                PhoneNumber = _configuration["AdminsData:PhoneNumber"]!,
                Email = _configuration["AdminsData:Email"]!,
                FinancialCode = _configuration["AdminsData:FinancialCode"]!,
                Password = _configuration["AdminsData:Password"]!,
                Role = _configuration["AdminsData:Role"]!
            };

            Create(user, user.Password); 
        }
    }

    public User Authenticate(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);

        // Проверка существования пользователя
        if (user == null)
        {
            return null!;
        }

        // Проверка соответствия пароля
        if (!_passwordHasher.VerifyPassword(user.Password, password))
        {
            return null!;
        }

        return user;
    }

    public User GetById(int id)
    {
        return _context.Users.Find(id)!;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User Create(User user, string password)
    {
        // Проверка существования пользователя с таким же email
        if (_context.Users.Any(u => u.Email == user.Email))
        {
            throw new ApplicationException("Email already exists");
        }

        // Хэширование пароля
        user.Password = _passwordHasher.HashPassword(password);

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public void Update(User user, string password = null!)
    {
        var existingUser = _context.Users.Find(user.Id);

        if (existingUser == null)
        {
            throw new ApplicationException("User not found");
        }

        // Обновление информации о пользователе
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.FinancialCode = user.FinancialCode;

        if (!string.IsNullOrWhiteSpace(password))
        {
            // Хэширование нового пароля
            existingUser.Password = _passwordHasher.HashPassword(password);
        }

        _context.Users.Update(existingUser);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            throw new ApplicationException("User not found");
        }

        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
