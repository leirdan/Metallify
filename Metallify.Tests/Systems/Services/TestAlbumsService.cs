﻿using FluentAssertions;
using Metallify.API.Config;
using Metallify.API.Models;
using Metallify.API.Services;
using Metallify.Tests.Fixtures;
using Metallify.Tests.Utils;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Metallify.Tests.Systems.Services;

public class TestAlbumsService
{
    private readonly string _endpoint = "https://example.com/albums";
    [Fact]
    public async Task GetAllAlbums_WhenCalled_InvokesHTTPGetRequest()
    {
        // Arrange -> this section "verifies" if the http request returns a expected data
        var config = Options.Create(new AlbumAPIOptions
        {
            Endpoint = _endpoint
        });
        var expected = AlbumsFixture.GetAlbumsForTests();
        var mock = MockHttpMessageHandler<Album>.SetupGetResourceList(expected);
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h, config);

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
        var config = Options.Create(new AlbumAPIOptions
        {
            Endpoint = _endpoint
        });
        var mock = MockHttpMessageHandler<Album>.SetupReturn404();
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h, config);

        // Act
        var result = await service.GetAllAlbums();

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllAlbums_WhenCalled_ReturnsListOfAlbumsOfExpectedSize()
    {
        // Arrange
        var config = Options.Create(new AlbumAPIOptions
        {
            Endpoint = _endpoint
        });
        var expected = AlbumsFixture.GetAlbumsForTests();
        var mock = MockHttpMessageHandler<Album>.SetupGetResourceList(expected);
        var h = new HttpClient(mock.Object);
        var service = new AlbumService(h, config);

        // Act
        var result = await service.GetAllAlbums();

        // Assert
        result.Count.Should().Be(expected.Count);
    }

    [Fact]
    public async Task GetAllAlbums_WhenCalled_InvokeExternalURL()
    {
        // Arrange
        var config = Options.Create(new AlbumAPIOptions
        {
            Endpoint = _endpoint
        });
        var expected = AlbumsFixture.GetAlbumsForTests();
        var mock = MockHttpMessageHandler<Album>.SetupGetResourceList(expected, _endpoint);
        var h = new HttpClient(mock.Object);
        
        var service = new AlbumService(h, config);

        // Act
        var result = await service.GetAllAlbums();
        var uri = new Uri(_endpoint);

        // Assert
        mock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(
                    req => 
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == uri
                    ),
                ItExpr.IsAny<CancellationToken>()
            ) ;
    }
}
