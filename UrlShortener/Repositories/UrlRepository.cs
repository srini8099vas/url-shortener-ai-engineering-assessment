using System.Text.Json;
using UrlShortener.Models;

namespace UrlShortener.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly string _filePath;
    private readonly string _clickFilePath;
    public UrlRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "urls.json");

        _clickFilePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "clicks.json");
    }

    public async Task<List<ShortUrl>> GetAllUrlsAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<ShortUrl>();
        }

        var json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<ShortUrl>>(json)
               ?? new List<ShortUrl>();
    }

    public async Task<ShortUrl?> GetByShortCodeAsync(string shortCode)
    {
        var urls = await GetAllUrlsAsync();

        return urls.FirstOrDefault(
            x => x.ShortCode.Equals(
                shortCode,
                StringComparison.OrdinalIgnoreCase));
    }

    public async Task AddUrlAsync(ShortUrl shortUrl)
    {
        var urls = await GetAllUrlsAsync();

        urls.Add(shortUrl);

        await SaveUrlsAsync(urls);
    }

    public async Task UpdateUrlsAsync(List<ShortUrl> urls)
    {
        await SaveUrlsAsync(urls);
    }

    public async Task DeleteUrlAsync(string shortCode)
    {
        var urls = await GetAllUrlsAsync();

        urls.RemoveAll(
            x => x.ShortCode.Equals(
                shortCode,
                StringComparison.OrdinalIgnoreCase));

        await SaveUrlsAsync(urls);
    }

    private async Task SaveUrlsAsync(List<ShortUrl> urls)
    {
        var json = JsonSerializer.Serialize(
            urls,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task AddClickAsync(UrlClick click)
    {
        var clicks = await GetAllClicksAsync();

        clicks.Add(click);

        await SaveClicksAsync(clicks);
    }

    public async Task<List<UrlClick>> GetClicksAsync(
        string shortCode)
    {
        var clicks = await GetAllClicksAsync();

        return clicks
            .Where(x => x.ShortCode == shortCode)
            .ToList();
    }

    private async Task<List<UrlClick>> GetAllClicksAsync()
    {
        if (!File.Exists(_clickFilePath))
        {
            return new List<UrlClick>();
        }

        var json =
            await File.ReadAllTextAsync(_clickFilePath);

        return JsonSerializer.Deserialize<List<UrlClick>>(json)
               ?? new List<UrlClick>();
    }

    private async Task SaveClicksAsync(
        List<UrlClick> clicks)
    {
        var json = JsonSerializer.Serialize(
            clicks,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await File.WriteAllTextAsync(
            _clickFilePath,
            json);
    }
}