using LibraryWebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;

namespace LibraryWebApi.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(int userId, JsonPatchDocument<User> user);
    Task<bool> DeleteUserAsync(int userId);
    Task<User> GetUserByIdAsync(int userId);
    User Authenticate(string email, string password);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<bool> AddBookToFavoritesAsync(int userId, int bookId);
    Task<bool> RemoveFromFavoritesAsync(int userId, int bookId);
//    Task<bool> AddBookRatingAsync(int userId, int bookId, int rating);
    Task<IEnumerable<User>> FilterUsersAsync(User filter);
    Task<IEnumerable<User>> SortUsersAsync(string sortBy);
}
 