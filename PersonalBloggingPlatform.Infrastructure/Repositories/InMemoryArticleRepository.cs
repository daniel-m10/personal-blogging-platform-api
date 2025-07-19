using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;

namespace PersonalBloggingPlatform.Infrastructure.Repositories;

public class InMemoryArticleRepository : IArticleRepository
{
    private readonly List<Article> _articles = [];

    public Task CreateAsync(Article article)
    {
        _articles.Add(article);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var article = _articles.FirstOrDefault(a => a.Id == id);
        if (article is not null)
        {
            _articles.Remove(article);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Article>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Article>>(_articles);
    }

    public Task<Article?> GetByIdAsync(Guid id)
    {
        var article = _articles.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(article);
    }

    public Task UpdateAsync(Article article)
    {
        var existing = _articles.FirstOrDefault(a => a.Id == article.Id);
        if (existing is not null)
        {
            _articles.Remove(existing);
            _articles.Add(article);
        }
        return Task.CompletedTask;
    }
}
