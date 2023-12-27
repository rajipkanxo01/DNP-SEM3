using System.Text.Json;
using Domains;

namespace BlazorServerApp.Data;

public class ChildHttpClient : IChildService
{
    private readonly HttpClient client;

    public ChildHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Child> CreateAsync(Child toCreateChild)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/childs", toCreateChild);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Child createdChild = JsonSerializer.Deserialize<Child>(result)!;
        return createdChild;
    }

    public async Task<ICollection<Child>> GetAllChilds(bool? favouriteFilerValue, string? genderFilter)
    {
        string query = ConstructQuery(favouriteFilerValue, genderFilter);

        HttpResponseMessage response = await client.GetAsync("/childs" + query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<Child> allChilds = JsonSerializer.Deserialize<ICollection<Child>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return allChilds;
    }

    private string ConstructQuery(bool? favouriteFilerValue, string? genderFilter)
    {
        string query = "";
        if (favouriteFilerValue != null)
        {
            query += $"?favouriteFilerValue={favouriteFilerValue}";
        }

        if (genderFilter != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"genderFilter={genderFilter}";
        }

        return query;
    }


    public async Task DeleteToyAsync(int toyId)
    {
        HttpResponseMessage responseMessage = await client.DeleteAsync($"/Childs/toy/{toyId}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}