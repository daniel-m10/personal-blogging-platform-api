using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application.Services
{
    [TestFixture]
    public class GetArticleByIdTests
    {
        [Test]
        public async Task Should_return_article_when_id_exists()
        {
            // Arrange
            var expectedArticle = FakeArticle.Get();

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(expectedArticle.Id)).ReturnsAsync(expectedArticle);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.GetByIdAsync(expectedArticle.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(expectedArticle.Id);
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
