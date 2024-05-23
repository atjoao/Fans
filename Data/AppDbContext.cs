using Microsoft.EntityFrameworkCore;

namespace Fans.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fans.Models.Posts> Posts { get; set; }
        public DbSet<Fans.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Fans.Models.Posts>().ToTable("Posts");
            modelBuilder.Entity<Fans.Models.User>().ToTable("Users");
        }
    }
}
