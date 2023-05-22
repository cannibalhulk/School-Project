namespace LibraryWebApi.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public DateTime DateCreated { get; set; }
    public byte[] File { get; set; } = null!;
 }
