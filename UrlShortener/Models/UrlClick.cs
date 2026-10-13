namespace UrlShortener.Models;

public class UrlClick
{
    public Guid Id { get; set; }

    public string ShortCode { get; set; } = string.Empty;

    public DateTime ClickedAt { get; set; }

    public string? IpAddress { get; set; }
}