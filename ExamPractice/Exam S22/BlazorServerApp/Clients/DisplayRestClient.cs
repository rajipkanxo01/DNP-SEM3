using System.Text.Json;
using Domains;

namespace BlazorServerApp.Clients;

public class DisplayRestClient : IDisplayClient
{
    private readonly HttpClient client;

    public DisplayRestClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<Image>> GetImagesAsync(string? albumCreator, string? topic)
    {
        string query = ConstructQuery(albumCreator, topic);

        HttpResponseMessage responseMessage = await client.GetAsync("Images" + query);
        string result = await responseMessage.Content.ReadAsStringAsync();


        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {result}");
        }

        ICollection<Image> allImages = JsonSerializer.Deserialize<ICollection<Image>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return allImages;
    }

    private string ConstructQuery(string? albumCreator, string? albumTopic)
    {
        string query = "";
        if (!string.IsNullOrEmpty(albumCreator))
        {
            query += $"?albumCreator={albumCreator}";
        }

        if (!string.IsNullOrEmpty(albumTopic))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"albumTopic={albumTopic}";
        }

        return query;
    }
}