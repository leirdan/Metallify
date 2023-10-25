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
        // now, the test 2nd test will work
        var albums = _albumService.GetAllAlbums();
        return Ok("all good");
    }
}
