using PersonalBloggingPlatform.Domain.Entities;

namespace PersonalBloggingPlatform.Domain.Interfaces;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAllAsync();
    Task<Article?> GetByIdAsync(Guid id);
    Task CreateAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(Guid id);
}