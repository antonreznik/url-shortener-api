using System.Data;
using Application.Interfaces;
using Domain;
using Infrastructure.UrlShortener;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UrlShortenerRepository(UrlShortenerDbContext dbContext) : IUrlShortenerRepository
    {
        public async Task<UrlShortenerModel> AddAsync(UrlShortenerModel model, CancellationToken cancellationToken = default)
        {
            var existing = await GetByOriginalUrlAsync(model.OriginalUrl, cancellationToken);
            
            if (existing is not null)
            {
                return existing;
            }

            await dbContext.UrlShorteners.AddAsync(model, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public Task<UrlShortenerModel?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            return dbContext.UrlShorteners
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ShortCode == shortCode, cancellationToken);
        }

        public Task<UrlShortenerModel?> GetByOriginalUrlAsync(string originalUrl, CancellationToken cancellationToken = default)
        {
            return dbContext.UrlShorteners
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl, cancellationToken);
        }

        public async Task<long> GetUrlIdSequenceAsync(CancellationToken cancellationToken = default)
        {
            var connection = dbContext.Database.GetDbConnection();
            var shouldOpenConnection = connection.State != ConnectionState.Open;

            if (shouldOpenConnection)
            {
                await connection.OpenAsync(cancellationToken);
            }

            try
            {
                await using var command = connection.CreateCommand();
                command.CommandText = "SELECT NEXT VALUE FOR UrlIdSequence";

                var result = await command.ExecuteScalarAsync(cancellationToken);
                return Convert.ToInt64(result);
            }
            finally
            {
                if (shouldOpenConnection)
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
