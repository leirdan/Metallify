using Metallify.API.Config;
using Metallify.API.Models;
using Microsoft.Extensions.Options;

namespace Metallify.API.Services;

public class AlbumService : IAlbumService
{
    private readonly HttpClient _httpClient;
    private readonly AlbumAPIOptions _apiConfig;

    public AlbumService(HttpClient h, IOptions<AlbumAPIOptions> apiConfig)
    {
        _httpClient = h;
        _apiConfig = apiConfig.Value;
    }
    public async Task<List<Album>> GetAllAlbums()
    {
        var res = await _httpClient.GetAsync(_apiConfig.Endpoint);
        if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<Album>();
        }
        var list = await res.Content.ReadFromJsonAsync<List<Album>>();
        return list.ToList();
    }

    public async Task<Album> GetSingleAlbum()
    {
        var album = new Album()
        {
            Id = 1,
            Title = "Litourgya",
            Genre = { Name = "Atmospheric Black Metal", Description = "The best of BM is here." }
        };
        return album;
    }
}
