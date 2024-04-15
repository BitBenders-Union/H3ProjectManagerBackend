using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;


namespace ProjectManagerBackend.Repo
{
    public class MappingService : IMappingService
    {
        public UserLogin UserLoginToDTO(UserLoginDTO userLogin)
        {

            UserLogin user = new()
            {
                Username = userLogin.Username,
                PasswordHash = userLogin.PasswordHash,
                PasswordSalt = userLogin.PasswordSalt,
                IsActive = true,
                UserDetail = new UserDetail()
            };

            return user;
        }
    }
}
