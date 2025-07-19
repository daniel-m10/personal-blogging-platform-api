using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Tests.Application
{
    [TestFixture]
    public class CreateArticleTests
    {
        [Test]
        public async Task Should_create_article_and_pass_to_repository()
        {
            // Arrange
            var mockRepo = new Mock<IArticleRepository>();
            var service = new ArticleService(mockRepo.Object);

            var title = "My First Post";
            var content = "This is the content";
            var tags = new List<string> { "csharp", "dotnet" };
            var author = "Juan Dev";

            // Act
            var article = await service.CreateAsync(title, content, tags, author);

            // Assert
            article.Title.Should().Be(title);
            article.Content.Should().Be(content);
            article.Author.Should().Be(author);
            article.Tags.Should().BeEquivalentTo(tags);

            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Article>()), Times.Once);
        }
    }
}
