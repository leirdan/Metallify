using Metallify.API.Models;

namespace Metallify.API.Services;

public class AlbumService : IAlbumService
{
    private readonly HttpClient _httpClient;
    public AlbumService(HttpClient h)
    {
        _httpClient = h;
    }
    public async Task<List<Album>> GetAllAlbums()
    {
        var res = await _httpClient.GetAsync("https://github.com/leirdan");
        return new List<Album>();
    }
}
