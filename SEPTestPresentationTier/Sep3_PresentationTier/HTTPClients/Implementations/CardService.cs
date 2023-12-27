using System.Net.Http.Json;
using System.Text.Json;
using Domains.Entity;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class CardService : ICardService
{
    private readonly HttpClient client;

    public CardService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<DebitCardEntity> CreateAsync(DebitCardEntity toCreateCardEntity)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("card/create", toCreateCardEntity);
        string result = await responseMessage.Content.ReadAsStringAsync();

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        DebitCardEntity debitCardEntity = JsonSerializer.Deserialize<DebitCardEntity>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return debitCardEntity;
    }

    public Task<DebitCardEntity> FetchCardByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<DebitCardEntity> UpdateCardAsync(long id, UserEntity userEntity)
    {
        throw new NotImplementedException();
    }

    public Task<Boolean> DeleteCardAsync(long id)
    {
        throw new NotImplementedException();
    }
}