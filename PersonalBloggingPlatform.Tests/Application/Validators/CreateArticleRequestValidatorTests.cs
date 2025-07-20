using FluentValidation.TestHelper;
using PersonalBloggingPlatform.Application.Contracts.Articles;
using PersonalBloggingPlatform.Application.Validation;

namespace PersonalBloggingPlatform.Tests.Application.Validators
{
    [TestFixture]
    public class CreateArticleRequestValidatorTests
    {
        private CreateArticleRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateArticleRequestValidator();
        }

        [Test]
        public void Should_Have_Error_When_Title_Is_Null_Or_Empty()
        {
            // Arrange
            var model = new CreateArticleRequest { Title = "" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Test]
        public void Should_Have_Error_When_Title_Is_Shorter_Than_3_Characters()
        {
            // Arrange
            var model = new CreateArticleRequest { Title = "ab" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Test]
        public void Should_Have_Error_When_Content_Is_Null_Or_Empty()
        {
            // Arrange
            var model = new CreateArticleRequest { Content = "" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Test]
        public void Should_Have_Error_When_Author_Is_Null_Or_Empty()
        {
            // Arrange
            var model = new CreateArticleRequest { Author = "" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Author);
        }

        [Test]
        public void Should_Not_Have_Error_When_Tags_Is_Null()
        {
            // Arrange
            var model = new CreateArticleRequest
            {
                Title = "Valid Title",
                Content = "Valid Content",
                Author = "Valid Author",
                Tags = null!
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Tags);
        }

        [Test]
        public void Should_Not_Have_Any_Errors_For_Valid_Request()
        {
            // Arrange
            var model = new CreateArticleRequest
            {
                Title = "Valid Title",
                Content = "Valid Content",
                Author = "Valid Author",
                Tags = ["tag1", "tag2"]
            };
            // Act
            var result = _validator.TestValidate(model);
            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
