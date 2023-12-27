namespace Models;

public class User
{
    public long UserId { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public DebitCard? Card { get; set; }

    public User(string? fullName, string? userName, string? password, DebitCard? card)
    {
        FullName = fullName;
        UserName = userName;

        Password = password;
        Card = card;
    }
}