using Microsoft.EntityFrameworkCore;
using PersonalBloggingPlatform.Domain.Entities;

namespace PersonalBloggingPlatform.Infrastructure.DataAccess
{
    public class BlogDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Article> Articles => Set<Article>();
    }
}

