using System.Collections;
using Domains.Entity;

namespace HTTPClients.ClientInterfaces;

public interface IUserService
{
    Task<UserEntity> CreateAsync(UserEntity toCreateUserEntity);
    Task<IEnumerable<UserEntity>> FetchUsersAsync();
    Task<UserEntity> FetchUserByIdAsync(long id);
    Task<UserEntity> FetchUserByUsernameAsync(string username);
    Task<UserEntity> UpdateUserAsync(long id, UserEntity userEntity);
    Task<Boolean> DeleteUserAsync(long id);
}