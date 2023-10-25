using Metallify.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Metallify.API.Services;
using Metallify.API.Models;

namespace Metallify.Tests.Systems.Controllers;

public class TestAlbumsController
{
    [Fact]
    // this one is okay
    public async void Get_OnSuccess_ReturnsStatusCode200()
    {
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(new List<Album>()
            {
                new()
                {
                    Id = 1,
                    Title = "Animals",
                    Genre = new () { Name = "Progressive Rock", Description = "Amazing!"}
                }
            });
        var c = new AlbumsController(mock.Object);
        var result = await c.Get() as OkObjectResult;
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    // now this one works: it's on "green" phase on "red/green/refactor"
    public async void Get_OnSuccess_InvokeAlbumServiceExactlyOnce()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(new List<Album>());
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
            .ReturnsAsync(new List<Album>()
            {
                new()
                {
                    Id = 1,
                    Title = "Animals",
                    Genre = new () { Name = "Progressive Rock", Description = "Amazing!"}
                }
            });

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
}