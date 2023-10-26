using Metallify.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Metallify.API.Services;
using Metallify.API.Models;
using Metallify.Tests.Fixtures;

namespace Metallify.Tests.Systems.Controllers;

public class TestAlbumsController
{
    [Fact]
    public async void Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(AlbumsFixture.GetAlbumsForTests());
        var c = new AlbumsController(mock.Object);

        // Act
        var result = await c.Get() as OkObjectResult;

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async void Get_OnSuccess_InvokeAlbumServiceExactlyOnce()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(AlbumsFixture.GetAlbumsForTests());
        var c = new AlbumsController(mock.Object);

        // Act
        var result = await c.Get();

        // Assert
        mock.Verify(s => s.GetAllAlbums(), Times.Once());
    }

    [Fact]
    public async void Get_OnSuccess_ReturnsListOfAlbums()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(AlbumsFixture.GetAlbumsForTests());

        var c = new AlbumsController(mock.Object);

        // Act
        var result = await c.Get();
        var obj = result as OkObjectResult;

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        obj.Value.Should().BeOfType<List<Album>>();
    }

    [Fact]
    public async void Get_OnNoAlbumsFound_ReturnsStatusCode404()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(new List<Album>());

        var c = new AlbumsController(mock.Object);

        // Act
        var result = await c.Get();
        var obj = result as NotFoundObjectResult;

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async void Get_OnSuccess_ReturnesASingleAlbum()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetSingleAlbum())
            .ReturnsAsync(AlbumsFixture.GetAlbumsForTests().First());

        var c = new AlbumsController(mock.Object);

        // Act
        var res = await c.GetOne();
        var obj = res as ObjectResult;

        // Assert
        res.Should().BeOfType<OkObjectResult>();
        obj.Value.Should().BeOfType<Album>();
    }

    [Fact]
    public async void Get_OnNoAlbumFound_ReturnsStatusCode404()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetSingleAlbum())
            .ReturnsAsync(new Album());
        var c = new AlbumsController(mock.Object);

        // Act
        var res = await c.GetOne();

        // Assert
        res.Should().BeOfType<NotFoundResult>();
    }
}