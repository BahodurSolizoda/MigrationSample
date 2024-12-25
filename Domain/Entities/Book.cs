namespace Domain.Entities;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public DateTime PublishYear { get; set; }
    public string Genre { get; set; }
    public bool IsAvailable { get; set; }
}