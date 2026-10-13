using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Services;

namespace UrlShortener.Controllers;

[ApiController]
[Route("api/url")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _service;

    public UrlController(IUrlService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUrlRequest request)
    {
        var result =
            await _service.CreateShortUrlAsync(request);

        return Ok(result);
    }
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> GetOriginalUrl(
        string shortCode)
    {
        var url =
            await _service.GetOriginalUrlAsync(
                shortCode,
                HttpContext.Connection.RemoteIpAddress?.ToString());

        return Ok(new
        {
            OriginalUrl = url
        });
    }

    [HttpGet("analytics/{shortCode}")]
    public async Task<IActionResult> Analytics(
        string shortCode)
    {
        var result =
            await _service.GetAnalyticsAsync(shortCode);

        return Ok(result);
    }

    [HttpDelete("{shortCode}")]
    public async Task<IActionResult> Delete(
        string shortCode)
    {
        await _service.DeleteAsync(shortCode);

        return NoContent();
    }
}