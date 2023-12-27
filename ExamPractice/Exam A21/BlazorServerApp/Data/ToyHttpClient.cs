using System.Text.Json;
using Domains;

namespace BlazorServerApp.Data;

public class ToyHttpClient : IToyService
{
    private readonly HttpClient client;

    public ToyHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(int selectedChildId, Toy toCreateToy)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/Toys/owner/{selectedChildId}", toCreateToy);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }
}