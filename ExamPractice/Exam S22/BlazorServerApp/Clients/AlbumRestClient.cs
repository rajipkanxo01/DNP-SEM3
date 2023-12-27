using System.Text.Json;
using Domains;

namespace BlazorServerApp.Clients;

public class AlbumRestClient : IAlbumClient
{
    private readonly HttpClient client;

    public AlbumRestClient(HttpClient client)
    {
        this.client = client;
    }


    public async Task<Album> CreateAlbumAsync(Album album)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/albums", album);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Album createdAlbum = JsonSerializer.Deserialize<Album>(result)!;
        return createdAlbum;
    }
}