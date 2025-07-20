using PersonalBloggingPlatform.Domain.Entities;

namespace PersonalBloggingPlatform.Tests.Application.Services
{
    public static class FakeArticle
    {
        public static Article Get() => new()
        {
            Id = Guid.NewGuid(),
            Title = "Sample Article",
            Content = "This is a sample article content.",
            PublishedAt = DateTime.UtcNow,
            Tags = new List<string> { "Sample", "Test" },
            Author = "Test Author"
        };
    }
}
