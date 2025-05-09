using IdeaManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdeaManager.Data.Db
{
    public class IdeaDbContext : DbContext
    {
        public IdeaDbContext(DbContextOptions<IdeaDbContext> options) : base(options) { }

        public DbSet<Idea> Ideas => Set<Idea>();
        public DbSet<User> Users => Set<User>();
    }
}

