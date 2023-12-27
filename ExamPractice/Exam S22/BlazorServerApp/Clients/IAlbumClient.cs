using Domains;

namespace BlazorServerApp.Clients;

public interface IAlbumClient
{
    Task<Album> CreateAlbumAsync(Album album);
}