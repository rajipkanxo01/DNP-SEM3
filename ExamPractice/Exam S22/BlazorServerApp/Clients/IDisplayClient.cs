using Domains;

namespace BlazorServerApp.Clients;

public interface IDisplayClient
{
    Task<ICollection<Image>> GetImagesAsync(string? albumCreator, string? topic);
}