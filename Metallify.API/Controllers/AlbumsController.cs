using Metallify.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metallify.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumService _albumService;
    public AlbumsController(IAlbumService a)
    {
        _albumService = a;
    }

    [HttpGet(Name = "GetAlbums")]
    public async Task<IActionResult> Get()
    {
        var albums = await _albumService.GetAllAlbums();
        if (albums is null || albums.Count == 0)
        {
            return NotFound();
        }
        return Ok(albums);
    }

    [HttpGet(Name = "GetAlbum")]
    public async Task<IActionResult> GetOne()
    {
        var album = await _albumService.GetSingleAlbum();
        if (album is null || album.Id == 0)
        {
            return NotFound();
        }
        return Ok(album);
    }
}
