using Domains;

namespace BlazorServerApp.Clients;

public interface IImageClient
{
    Task<Image> AddImageToAlbumAsync(Image img, string albumTitle);
    Task<ICollection<string>> GetAlbumTitlesAsync();
}