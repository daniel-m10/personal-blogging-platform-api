using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Application.Services;

public class ArticleService(IArticleRepository repository)
{
    private readonly IArticleRepository _repository = repository;

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        var articles = await _repository.GetAllAsync();
        return articles.OrderByDescending(a => a.PublishedAt);
    }

    public async Task<Article?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

    public async Task<Article> CreateAsync(string title, string content, List<string> tags, string author)
    {
        var article = new Article
        {
            Title = title,
            Content = content,
            Tags = tags,
            Author = author
        };

        await _repository.CreateAsync(article);
        return article;
    }

    public async Task<Article?> UpdateAsync(Guid guid, string title, string content, List<string> tags)
    {
        var article = await _repository.GetByIdAsync(guid);

        if (article == null)
        {
            return null;
        }

        article.Title = title;
        article.Content = content;
        article.Tags = tags;

        await _repository.UpdateAsync(article);
        return article;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
        {
            return false;
        }

        await _repository.DeleteAsync(id);
        return true;
    }
}
