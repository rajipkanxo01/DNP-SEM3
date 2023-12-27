namespace Domains.Entity;

public class UserEntity
{
    public long UserId { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public int Balance { get; set; }
    public DebitCardEntity? Card { get; set; }

    public UserEntity(string? fullName, string? userName, string? password, DebitCardEntity? card, int balance)
    {
        FullName = fullName;
        UserName = userName;
        Password = password;
        Balance = balance;
        Card = card;
    }
}