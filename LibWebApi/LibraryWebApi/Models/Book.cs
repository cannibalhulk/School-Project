using static Bogus.DataSets.Name;

namespace LibraryWebApi.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime AddedDate { get; set; }
    public byte[] FileData { get; set; } = null!;
    public byte[] PhotoData { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public bool PrivateStatus { get; set; }
    //public List<string> Hashtags { get; set; }

#pragma warning disable CS8618
    //public Book() => Hashtags = new List<string>();
#pragma warning restore CS8618 
}
