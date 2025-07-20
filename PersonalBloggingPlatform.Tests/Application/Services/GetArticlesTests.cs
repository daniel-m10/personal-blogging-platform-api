using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application.Services
{
    [TestFixture]
    public class GetArticlesTests
    {
        [Test]
        public async Task Should_return_articles_ordered_by_date_descending()
        {
            // Arrange
            var articles = new List<Article>
            {
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Older Article",
                    Content = "This is an older article.",
                    PublishedAt = DateTime.UtcNow.AddDays(-10),
                    Tags = ["Old", "Article"],
                    Author = "Author1"
                },
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Newest Article",
                    Content = "This is the newest article.",
                    PublishedAt = DateTime.UtcNow,
                    Tags = ["New", "Article"],
                    Author = "Author2"
                }
            };

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(articles);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            result.Should().BeInDescendingOrder(a => a.PublishedAt);
        }
    }
}
