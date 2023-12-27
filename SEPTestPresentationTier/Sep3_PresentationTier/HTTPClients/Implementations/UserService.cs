using System.Net.Http.Json;
using System.Text.Json;
using Domains.Entity;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class UserService : IUserService
{
    private readonly HttpClient client;

    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<UserEntity> CreateAsync(UserEntity toCreateUserEntity)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/user/create", toCreateUserEntity);
        string result = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        UserEntity userEntity = JsonSerializer.Deserialize<UserEntity>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return userEntity;
    }

    public Task<IEnumerable<UserEntity>> FetchUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> FetchUserByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> FetchUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> UpdateUserAsync(long id, UserEntity userEntity)
    {
        throw new NotImplementedException();
    }

    public Task<Boolean> DeleteUserAsync(long id)
    {
        throw new NotImplementedException();
    }
}