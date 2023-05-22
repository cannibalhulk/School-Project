using LibraryWebApi.Models;
using System.Security.Claims;

namespace LibraryWebApi.Services.Interfaces;

public interface IUserService
{
    User Authenticate(string email, string password);
    User GetById(int id);
    IEnumerable<User> GetAll();
    User Create(User user, string password);
    void Update(User user, string password = null!);
    void Delete(int id);
}