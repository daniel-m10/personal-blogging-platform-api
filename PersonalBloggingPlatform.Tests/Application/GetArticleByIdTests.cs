using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application
{
    [TestFixture]
    public class GetArticleByIdTests
    {
        [Test]
        public async Task Should_return_article_when_id_exists()
        {
            // Arrange
            var articleId = Guid.NewGuid();
            var expectedArticle = new Article
            {
                Id = articleId,
                Title = "Test Article",
                PublishedAt = DateTime.UtcNow,
            };

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(articleId)).ReturnsAsync(expectedArticle);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.GetByIdAsync(articleId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(articleId);
        }

        [Test]
        public async Task Should_return_null_when_article_does_not_exist()
        {
            // Arrange
            var articleId = Guid.NewGuid();

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(articleId)).ReturnsAsync((Article?)null);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.GetByIdAsync(articleId);

            // Assert
            result.Should().BeNull();
        }
    }
}
