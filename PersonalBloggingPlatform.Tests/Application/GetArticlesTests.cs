using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application
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
                new() {Title = "Old Article", PublishedAt = DateTime.UtcNow.AddDays(-10)},
                new() {Title = "New Article", PublishedAt = DateTime.UtcNow}
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
