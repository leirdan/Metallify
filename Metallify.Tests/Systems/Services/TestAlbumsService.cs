using FluentAssertions;
using Metallify.API.Models;
using Metallify.API.Services;
using Metallify.Tests.Fixtures;
using Metallify.Tests.Utils;
using Moq;
using Moq.Protected;

namespace Metallify.Tests.Systems.Services;

public class TestAlbumsService
{
    [Fact]
    public async Task GetAllAlbums_WhenCalled_InvokesHTTPGetRequest()
    {
        // Arrange -> this section "verifies" if the http request returns a expected data
        var expected = AlbumsFixture.GetAlbumsForTests();
        var mock = MockHttpMessageHandler<Album>.SetupGetResourceList(expected);
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h);

        // Act
        await service.GetAllAlbums();

        // Assert -> Verify if HTTP Request was made
        mock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task GetAllAlbums_WhenCalled_ReturnsEmptyListOfAlbums()
    {
        // Arrange
        var mock = MockHttpMessageHandler<Album>.SetupReturn404();
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h);
        
        // Act
        var result = await service.GetAllAlbums();

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllAlbums_WhenCalled_ReturnsListOfAlbumsOfExpectedSize()
    {
        // Arrange
        var expected = AlbumsFixture.GetAlbumsForTests();
        var mock = MockHttpMessageHandler<Album>.SetupGetResourceList(expected);
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h);

        // Act
        var result = await service.GetAllAlbums();

        // Assert
        result.Count.Should().Be(expected.Count);
    }

    [Fact]
    public async Task GetAllAlbums_WhenCalled_InvokeExternalURL()
    {
        // Arrange
        var expected = AlbumsFixture.GetAlbumsForTests();
        var mock = MockHttpMessageHandler<Album>.SetupGetResourceList(expected);
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h);

        // Act
        var result = await service.GetAllAlbums();

        // Assert
        result.Count.Should().Be(expected.Count);
    }
}
