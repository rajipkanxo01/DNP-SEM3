using System.Text.Json;
using Domains;

namespace BlazorServerApp.Clients;

public class ImageRestClient : IImageClient
{
    private readonly HttpClient client;

    public ImageRestClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Image> AddImageToAlbumAsync(Image img, string albumTitle)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/albums/{albumTitle}", img);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Image image = JsonSerializer.Deserialize<Image>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return image;
    }


    public async Task<ICollection<string>> GetAlbumTitlesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Albums/titles");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<string> allTitles = JsonSerializer.Deserialize<ICollection<string>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return allTitles;
    }
}