namespace UrlShortener.DTOs;

public class AnalyticsResponse
{
    public string ShortCode { get; set; } = string.Empty;

    public int ClickCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<DateTime> ClickHistory { get; set; }
        = new();
}