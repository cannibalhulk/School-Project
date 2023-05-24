using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    public string PersonalCode { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public List<FavoriteBook> FavoriteBooks { get; set; } = null!;

}
