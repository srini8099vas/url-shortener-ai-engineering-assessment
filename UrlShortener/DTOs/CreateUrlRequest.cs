namespace UrlShortener.DTOs;

public class CreateUrlRequest
{
    public string OriginalUrl { get; set; } = string.Empty;

    public string? CustomCode { get; set; }

    public int? ExpiryDays { get; set; }
}
