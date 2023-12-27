using Domains;

namespace WebAPI.Data;

public interface IDataAccess
{
    Task<Album> AddAlbumAsync(Album album);
    Task<ICollection<string>> GetAlbumTitlesAsync();
    Task<Image> AddImageToAlbumAsync(string albumTitle, Image img);
    Task<ICollection<Image>> GetImagesAsync(string? albumCreator, string? topic);
}