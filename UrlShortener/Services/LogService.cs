namespace UrlShortener.Services;

public class LogService : ILogService
{
    private readonly IWebHostEnvironment _environment;

    public LogService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task LogAsync(string message)
    {
        var logPath = Path.Combine(
            _environment.ContentRootPath,
            "Logs",
            "app.log");

        var logMessage =
            $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | {message}{Environment.NewLine}";

        await File.AppendAllTextAsync(
            logPath,
            logMessage);
    }
}