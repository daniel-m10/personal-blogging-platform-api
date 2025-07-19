using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application
{
    [TestFixture]
    public class UpdateArticleTests
    {
        [Test]
        public async Task Should_update_article_When_it_exists()
        {
            // Arrange
            var existing = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Old Title",
                Content = "Old Content",
                Author = "Juan",
                Tags = ["old"],
                PublishedAt = DateTime.UtcNow
            };

            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(existing.Id)).ReturnsAsync(existing);
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Article>()));

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.UpdateAsync(
                existing.Id,
                "New Title",
                "New Content",
                ["new", "tags"]
            );

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("New Title");
            result.Tags.Should().Contain("new");
        }

        [Test]
        public async Task Should_return_null_when_article_not_found()
        {
            // Arrange
            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Article?)null);

            var service = new ArticleService(mockRepo.Object);

            // Act
            var result = await service.UpdateAsync(
                Guid.NewGuid(),
                "New Title",
                "New Content",
                ["new", "tags"]
            );

            // Assert
            result.Should().BeNull();
        }
    }
}
