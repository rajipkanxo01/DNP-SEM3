namespace Domains.Entity;

public class DebitCardEntity
{
    public long CardId { get; set; }
    public long CardNumber { get; set; }
    public string? ExpiryDate { get; set; }
    public int Cvv { get; set; }

    public DebitCardEntity(long cardNumber, string? expiryDate, int cvv)
    {
        CardNumber = cardNumber;
        ExpiryDate = expiryDate;
        Cvv = cvv;
    }
}