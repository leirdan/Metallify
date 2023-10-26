using Metallify.API.Models;

namespace Metallify.API.Services;

public interface IAlbumService
{
    public Task<List<Album>> GetAllAlbums();
    public Task<Album> GetSingleAlbum();
}
