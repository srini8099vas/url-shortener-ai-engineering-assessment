using FluentAssertions;
using Moq;
using UrlShortener.DTOs;
using UrlShortener.Repositories;
using UrlShortener.Services;

namespace UrlShortener.Tests.Services;

public class UrlServiceTests
{
    private readonly Mock<IUrlRepository> _repositoryMock;
    private readonly Mock<ILogService> _logServiceMock;
    private readonly UrlService _service;

    public UrlServiceTests()
    {
        _repositoryMock =
            new Mock<IUrlRepository>();

        _logServiceMock =
            new Mock<ILogService>();

        _service = new UrlService(
            _repositoryMock.Object,
            _logServiceMock.Object);
    }

    [Fact]
    public async Task CreateShortUrlAsync_Should_Create_Url()
    {
        var request =
            new CreateUrlRequest
            {
                OriginalUrl =
                    "https://google.com"
            };

        _repositoryMock
            .Setup(x => x.GetAllUrlsAsync())
            .ReturnsAsync(new List<Models.ShortUrl>());

        var result =
            await _service.CreateShortUrlAsync(
                request);

        result.Should().NotBeNull();

        result.ShortCode.Should()
            .NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task CreateShortUrlAsync_Should_Throw_For_Invalid_Url()
    {
        var request =
            new CreateUrlRequest
            {
                OriginalUrl = "abcd"
            };

        Func<Task> action =
            async () =>
                await _service.CreateShortUrlAsync(
                    request);

        await action.Should()
            .ThrowAsync<Exception>();
    }

    [Fact]
    public async Task CreateShortUrlAsync_Should_Use_Custom_Code()
    {
        var request =
            new CreateUrlRequest
            {
                OriginalUrl =
                    "https://google.com",

                CustomCode = "google"
            };

        _repositoryMock
            .Setup(x => x.GetAllUrlsAsync())
            .ReturnsAsync(new List<Models.ShortUrl>());

        var result =
            await _service.CreateShortUrlAsync(
                request);

        result.ShortCode
            .Should()
            .Be("google");
    }

    [Fact]
    public async Task CreateShortUrlAsync_Should_Throw_When_Custom_Code_Exists()
    {
        _repositoryMock
            .Setup(x => x.GetAllUrlsAsync())
            .ReturnsAsync(
            [
                new Models.ShortUrl
            {
                ShortCode = "google"
            }
            ]);

        var request =
            new CreateUrlRequest
            {
                OriginalUrl =
                    "https://google.com",

                CustomCode = "google"
            };

        Func<Task> action =
            async () =>
                await _service.CreateShortUrlAsync(
                    request);

        await action.Should()
            .ThrowAsync<Exception>();
    }

    [Fact]
    public async Task CreateShortUrlAsync_Should_Set_Expiry_Date()
    {
        var request = new CreateUrlRequest
        {
            OriginalUrl = "https://google.com",
            ExpiryDays = 30
        };

        _repositoryMock
            .Setup(x => x.GetAllUrlsAsync())
            .ReturnsAsync(new List<UrlShortener.Models.ShortUrl>());

        var result =
            await _service.CreateShortUrlAsync(request);

        result.ExpiryDate.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAnalyticsAsync_Should_Return_Data()
    {
        var shortUrl =
            new UrlShortener.Models.ShortUrl
            {
                ShortCode = "google",
                ClickCount = 5,
                CreatedAt = DateTime.UtcNow
            };

        _repositoryMock
            .Setup(x => x.GetByShortCodeAsync("google"))
            .ReturnsAsync(shortUrl);

        _repositoryMock
            .Setup(x => x.GetClicksAsync("google"))
            .ReturnsAsync(new List<UrlShortener.Models.UrlClick>());

        var result =
            await _service.GetAnalyticsAsync("google");

        result.ClickCount.Should().Be(5);
    }

    [Fact]
    public async Task DeleteAsync_Should_Call_Repository()
    {
        await _service.DeleteAsync("google");

        _repositoryMock.Verify(
            x => x.DeleteUrlAsync("google"),
            Times.Once);
    }

    [Fact]
    public async Task GetAnalyticsAsync_Should_Throw_When_Not_Found()
    {
        _repositoryMock
            .Setup(x => x.GetByShortCodeAsync("badcode"))
            .ReturnsAsync((UrlShortener.Models.ShortUrl?)null);

        Func<Task> action =
            async () =>
                await _service.GetAnalyticsAsync("badcode");

        await action.Should()
            .ThrowAsync<Exception>();
    }
}