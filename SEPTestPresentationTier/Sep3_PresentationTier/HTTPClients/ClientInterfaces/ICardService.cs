using Domains.Entity;

namespace HTTPClients.ClientInterfaces;

public interface ICardService
{
    Task<DebitCardEntity> CreateAsync(DebitCardEntity toCreateCardEntity);
    Task<DebitCardEntity> FetchCardByUsernameAsync(string username);
    Task<DebitCardEntity> UpdateCardAsync(long id, UserEntity userEntity);
    Task<Boolean> DeleteCardAsync(long id);
}