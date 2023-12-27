using System.Security.Claims;
using System.Text.Json;
using Shared.Dtos;
using Shared.Model;

namespace BlazorServerApp.Services.Http;

public class JwtAuthService : IAuthService
{
    private readonly HttpClient client = new();

    // This will hold the token received from the Web API when logged in
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }


    public async Task LoginAsync(string username, string password)
    {
        UserLoginDto userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        HttpResponseMessage responseMessage =
            await client.PostAsJsonAsync("https://localhost:7067/Auth/login", userLoginDto);
        // the content is jwt 
        string responseContent = await responseMessage.Content.ReadAsStringAsync();


        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;

        ClaimsPrincipal principal = CreateClaimsPrincipal();
        OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (!string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);

        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
}