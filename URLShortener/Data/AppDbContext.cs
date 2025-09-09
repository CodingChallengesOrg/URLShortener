namespace URLShortener.Data
{
    using Microsoft.EntityFrameworkCore;
    using URLShortener.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UrlMap> UrlMaps { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlMap>()
                .HasIndex(u => u.ShortUrl)
                .IsUnique();
        }
    }
}
