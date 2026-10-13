using UrlShortener.DTOs;

namespace UrlShortener.Services;

public interface IUrlService
{
    Task<CreateUrlResponse> CreateShortUrlAsync(
        CreateUrlRequest request);

    Task<string> GetOriginalUrlAsync(
    string shortCode,
    string? ipAddress);


    Task<AnalyticsResponse> GetAnalyticsAsync(
        string shortCode);

    Task DeleteAsync(
        string shortCode);
}