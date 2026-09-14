using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class UrlShortenerModelConfiguration : IEntityTypeConfiguration<UrlShortenerModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UrlShortenerModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => x.OriginalUrl);

            builder.HasIndex(x => x.ShortCode).IsUnique();
        }
    }
}
