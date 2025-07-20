namespace PersonalBloggingPlatform.Domain.Entities;

public record Article
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime PublishedAt { get; init; }
    public List<string>? Tags { get; set; }
    public required string Author { get; set; }
}
