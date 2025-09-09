using Microsoft.EntityFrameworkCore;
using URLShortener.Data;
using URLShortener.Models;
using URLShortener.Utils;

namespace URLShortener.Services
{
    using Microsoft.EntityFrameworkCore;

    public class UrlService
    {
        private readonly AppDbContext dbContext;

        public UrlService(AppDbContext context)
        {
            dbContext = context;
        }

        public async Task<UrlMap> CreateShortUrlAsync(string longUrl)
        {
            // Step 1: Insert the record with OriginalUrl only
            var entity = new UrlMap { OriginalUrl = longUrl };
            dbContext.UrlMaps.Add(entity);
            await dbContext.SaveChangesAsync(); // get auto-increment ID

            // Step 2: Try Base62 encoding of Id
            var shortUrl = Base62Encoder.Encode(entity.Id);
            entity.ShortUrl = shortUrl;

            try
            {
                dbContext.UrlMaps.Update(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Collision occurred → fallback to hash
                entity.ShortUrl = HashHelper.GenerateHash(longUrl + Guid.NewGuid());
                dbContext.UrlMaps.Update(entity);
                await dbContext.SaveChangesAsync();
            }

            return entity;
        }

        // 🔹 Resolve using Decode (fastest path)
        public async Task<UrlMap?> ResolveAsync(string shortUrl)
        {
            try
            {
                // Step 1: Try decode
                long id = Base62Encoder.Decode(shortUrl);

                // Step 2: Try direct fetch by ID
                var entity = await dbContext.UrlMaps.FindAsync(id);

                // Step 3: Validate ShortUrl (in case fallback hash was used)
                if (entity != null && entity.ShortUrl == shortUrl)
                    return entity;
            }
            catch
            {
                // If decode fails, it's not Base62
            }

            // Fallback: lookup by ShortUrl (hash-generated case)
            return await dbContext.UrlMaps
                .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
        }
    }
}
