using FluentValidation;
using PersonalBloggingPlatform.Application.Contracts.Articles;

namespace PersonalBloggingPlatform.Application.Validation
{
    public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
    {
        public UpdateArticleRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("Content must be at least 10 characters.");

            RuleFor(x => x.Tags)
                .Must(tags => tags == null || tags.Count <= 5)
                .WithMessage("No more than 5 tags allowed.")
                .ForEach(tagRule =>
                {
                    tagRule.MaximumLength(30).WithMessage("Each tag must be at most 30 characters.");
                });
        }
    }
}
