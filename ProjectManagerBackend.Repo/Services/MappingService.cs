using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;


namespace ProjectManagerBackend.Repo
{
    public class MappingService : IMappingService
    {
        private readonly IHashingService hashingService;

        public MappingService(IHashingService hashingService)
        {
            this.hashingService = hashingService;
        }
        public UserLogin UserLoginToDTO(UserLoginDTO userLoginDTO)
        {
            byte[] salt = hashingService.GenerateSalt();
            byte[] hash = hashingService.PasswordHashing(userLoginDTO.Password, salt);

            UserLogin user = new()
            {
                Username = userLoginDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            return user;
        }
    }
}
