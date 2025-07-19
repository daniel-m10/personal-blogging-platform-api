namespace PersonalBloggingPlatform.Domain.Entities;

public class Article
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedAt { get; init; } = DateTime.UtcNow;
    public List<string> Tags { get; set; } = [];
    public string Author { get; set; } = string.Empty;
}
