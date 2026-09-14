using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UrlShortener
{
    public class UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : DbContext(options)
    {
        public DbSet<UrlShortenerModel> UrlShorteners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>("UrlIdSequence")
                .StartsAt(10_000)
                .IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(UrlShortenerDbContext).Assembly);
        }

    }
}
