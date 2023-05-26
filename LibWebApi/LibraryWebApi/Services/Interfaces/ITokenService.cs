using LibraryWebApi.Models;

namespace LibraryWebApi.Services.Interfaces;
//
public interface ITokenService
{
    string GenerateToken(User user);
}
