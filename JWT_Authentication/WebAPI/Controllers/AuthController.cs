using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using Shared.Model;
using WebAPI.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;

    public AuthController(IConfiguration config, IAuthService authService)
    {
        this.config = config;
        this.authService = authService;
    }

    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("DisplayName", user.Name),
            new Claim("Email", user.Email),
            new Claim("Age", user.Age.ToString()),
            new Claim("Domain", user.Domain),
            new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };
        return claims.ToList();
    }

    private string GenerateJwt(User user)
    {
        List<Claim> generatedClaims = GenerateClaims(user);
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new JwtHeader(signIn);

        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            generatedClaims,
            null,
            DateTime.UtcNow.AddMinutes(60));

        JwtSecurityToken token = new JwtSecurityToken(header, payload);

        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            User user = await authService.ValidateUser(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwt(user);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}