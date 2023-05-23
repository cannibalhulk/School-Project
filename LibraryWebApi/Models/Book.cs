using static Bogus.DataSets.Name;

namespace LibraryWebApi.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public string ImageUrl { get; set; }
    //public List<string> Hashtags { get; set; }
    public string Genre { get; set; }
    public bool PrivateStatus { get; set; }

#pragma warning disable CS8618 
    //public Book() => Hashtags = new List<string>();
#pragma warning restore CS8618
}

