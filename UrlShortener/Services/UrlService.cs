using UrlShortener.DTOs;
using UrlShortener.Models;
using UrlShortener.Repositories;

namespace UrlShortener.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _repository;
    private readonly ILogService _logService;

    public UrlService(
        IUrlRepository repository,
        ILogService logService)
    {
        _repository = repository;
        _logService = logService;
    }

    public async Task<CreateUrlResponse> CreateShortUrlAsync(
    CreateUrlRequest request)
    {
        if (!Uri.IsWellFormedUriString(
            request.OriginalUrl,
            UriKind.Absolute))
        {
            throw new Exception("Invalid URL.");
        }

        var allUrls =
            await _repository.GetAllUrlsAsync();

        string code;

        if (!string.IsNullOrWhiteSpace(
            request.CustomCode))
        {
            var exists = allUrls.Any(x =>
                x.ShortCode.Equals(
                    request.CustomCode,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                throw new Exception(
                    "Custom code already exists.");
            }

            code = request.CustomCode;
        }
        else
        {
            code = Guid.NewGuid()
                       .ToString("N")
                       .Substring(0, 6);
        }

        DateTime? expiryDate = null;

        if (request.ExpiryDays.HasValue)
        {
            expiryDate =
                DateTime.UtcNow.AddDays(
                    request.ExpiryDays.Value);
        }

        var url = new ShortUrl
        {
            Id = Guid.NewGuid(),
            OriginalUrl = request.OriginalUrl,
            ShortCode = code,
            CreatedAt = DateTime.UtcNow,
            ClickCount = 0,
            ExpiryDate = expiryDate
        };

        await _repository.AddUrlAsync(url);

        await _logService.LogAsync(
    $"URL Created | Code={code}");

        return new CreateUrlResponse
        {
            ShortCode = code,
            ShortUrl =
                $"https://localhost:7241/api/url/{code}",
            ExpiryDate = expiryDate
        };
    }
    public async Task<string> GetOriginalUrlAsync(
     string shortCode,
     string? ipAddress)
    {
        var url =
            await _repository.GetByShortCodeAsync(
                shortCode);

        if (url == null)
        {
            throw new Exception(
                "Short URL not found.");
        }

        if (url.ExpiryDate.HasValue &&
            url.ExpiryDate.Value < DateTime.UtcNow)
        {
            throw new Exception(
                "URL has expired.");
        }

        var urls =
            await _repository.GetAllUrlsAsync();

        var existing =
            urls.First(x =>
                x.ShortCode == shortCode);

        existing.ClickCount++;

        await _repository.UpdateUrlsAsync(urls);

        await _repository.AddClickAsync(
            new Models.UrlClick
            {
                Id = Guid.NewGuid(),
                ShortCode = shortCode,
                ClickedAt = DateTime.UtcNow,
                IpAddress = ipAddress
            });

        await _logService.LogAsync(
    $"URL Accessed | Code={shortCode}");

        return existing.OriginalUrl;
    }

    public async Task<AnalyticsResponse> GetAnalyticsAsync(
    string shortCode)
    {
        var url =
            await _repository.GetByShortCodeAsync(shortCode);

        if (url == null)
        {
            throw new Exception(
                "Short URL not found.");
        }

        var clicks =
            await _repository.GetClicksAsync(shortCode);

        await _logService.LogAsync(
    $"Analytics Viewed | Code={shortCode}");

        return new AnalyticsResponse
        {
            ShortCode = url.ShortCode,
            ClickCount = url.ClickCount,
            CreatedAt = url.CreatedAt,
            ClickHistory = clicks
                .Select(x => x.ClickedAt)
                .ToList()
        };
    }

    public async Task DeleteAsync(
        string shortCode)
    {


        await _logService.LogAsync(
            $"URL Deleted | Code={shortCode}");

        await _repository.DeleteUrlAsync(shortCode);
    }
}