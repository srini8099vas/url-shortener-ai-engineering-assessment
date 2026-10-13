namespace UrlShortener.DTOs;

public class CreateUrlResponse
{
    public string ShortCode { get; set; } = string.Empty;

    public string ShortUrl { get; set; } = string.Empty;

    public DateTime? ExpiryDate { get; set; }
}