namespace UrlShortener.Services;

public interface ILogService
{
    Task LogAsync(string message);
}