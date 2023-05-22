namespace LibraryWebApi.Models;

public class Rating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Value { get; set; }
}