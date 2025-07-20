using Microsoft.EntityFrameworkCore;
using PersonalBloggingPlatform.Domain.Entities;
using PersonalBloggingPlatform.Domain.Interfaces;
using PersonalBloggingPlatform.Infrastructure.DataAccess;

namespace PersonalBloggingPlatform.Infrastructure.Repositories
{
    public class EfArticleRepository(BlogDbContext context) : IArticleRepository
    {
        private readonly BlogDbContext _context = context;

        public async Task CreateAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles.OrderByDescending(a => a.PublishedAt).ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(Guid id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            return _context.SaveChangesAsync();
        }
    }
}
