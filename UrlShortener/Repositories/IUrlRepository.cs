using UrlShortener.Models;

namespace UrlShortener.Repositories;

public interface IUrlRepository
{
    Task<List<ShortUrl>> GetAllUrlsAsync();

    Task<ShortUrl?> GetByShortCodeAsync(string shortCode);

    Task AddUrlAsync(ShortUrl shortUrl);

    Task UpdateUrlsAsync(List<ShortUrl> urls);

    Task DeleteUrlAsync(string shortCode);

    Task AddClickAsync(UrlClick click);

    Task<List<UrlClick>> GetClicksAsync(string shortCode);
}