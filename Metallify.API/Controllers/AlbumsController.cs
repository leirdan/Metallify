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

    [HttpGet(Name = "GetAlbum")]
    public async Task<IActionResult> Get()
    {
        return Ok("all good");
    }
}
