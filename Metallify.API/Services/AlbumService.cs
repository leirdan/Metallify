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
        if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<Album>();
        }
        var list = await res.Content.ReadFromJsonAsync<List<Album>>();
        return list.ToList();
    }
}
