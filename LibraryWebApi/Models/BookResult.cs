namespace LibraryWebApi.Models;

public class BookResult
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<Book> Books { get; set; } = null!;
}