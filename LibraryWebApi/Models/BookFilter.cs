namespace LibraryWebApi.Models;

public class BookFilter
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime DateAdded { get; set; } 
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public List<string> Hashtags { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public bool PrivateStatus { get; set; }
}