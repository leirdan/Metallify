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
        var c = new AlbumsController(new Mock<IAlbumService>().Object);
        var result = await c.Get() as OkObjectResult;
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    // this one is failing because it's on the "red" phase of "red/green/refactor" TDD
    public async void Get_OnSuccess_InvokeAlbumServiceExactlyOnce()
    {
        // Arrange
        var mock = new Mock<IAlbumService>();
        mock
            .Setup(s => s.GetAllAlbums())
            .ReturnsAsync(new List<Album>());
        var c = new AlbumsController(mock.Object);
        // Act
        var result = await c.Get() as OkObjectResult;
        // Assert
        mock.Verify(s => s.GetAllAlbums(), Times.Once());
    }
}