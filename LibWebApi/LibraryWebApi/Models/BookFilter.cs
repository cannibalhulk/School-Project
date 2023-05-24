namespace LibraryWebApi.Models;

public class BookFilter
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime AddedDate { get; set; } 
    public DateTime CreatedDate { get; set; }
    public List<string> Hashtags { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public bool PrivateStatus { get; set; }
}