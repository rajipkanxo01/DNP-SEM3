namespace Shared.Dtos;

public class UserLoginDto
{
    // init means value can be set only when object is created but can not modify later 
    public string Username { get; init; }
    public string Password { get; init; }    
}