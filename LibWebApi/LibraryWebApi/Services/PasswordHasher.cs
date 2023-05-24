using System.Security.Cryptography;
using System.Text;
using LibraryWebApi.Services.Interfaces;

namespace LibraryWebApi.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        // Хэширование пароля
        byte[] salt;
        byte[] hash;

        using (var hmac = new HMACSHA512())
        {
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        // Проверка соответствия пароля и хэша
        var passwordParts = hashedPassword.Split('|');
        if (passwordParts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(passwordParts[0]);
        var hash = Convert.FromBase64String(passwordParts[1]);

        using (var hmac = new HMACSHA512(salt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hash[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
