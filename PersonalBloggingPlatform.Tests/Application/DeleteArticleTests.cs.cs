using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application
{
    [TestFixture]
    public class DeleteArticleTests
    {
        [Test]
        public async Task Should_delete_article_when_it_exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var article = new Article { Id = id };

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(article);
            mockRepo.Setup(repo => repo.DeleteAsync(id)).Returns(Task.CompletedTask);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
            mockRepo.Verify(repo => repo.DeleteAsync(id), Times.Once);
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
