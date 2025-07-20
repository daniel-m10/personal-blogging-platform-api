using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application.Services
{
    [TestFixture]
    public class DeleteArticleTests
    {
        [Test]
        public async Task Should_delete_article_when_it_exists()
        {
            // Arrange
            var article = FakeArticle.Get();

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(article.Id)).ReturnsAsync(article);
            mockRepo.Setup(repo => repo.DeleteAsync(article.Id)).Returns(Task.CompletedTask);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.DeleteAsync(article.Id);

            // Assert
            result.Should().BeTrue();
            mockRepo.Verify(repo => repo.DeleteAsync(article.Id), Times.Once);
        }

        [Test]
        public async Task Should_return_false_when_article_does_not_exist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Article?)null);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.DeleteAsync(id);

            // Assert
            result.Should().BeFalse();
            mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
