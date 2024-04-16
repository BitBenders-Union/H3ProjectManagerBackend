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
        public UserLogin AddUser(UserDetailDTO userDetailDTO)
        {
            byte[] salt = hashingService.GenerateSalt();
            byte[] hash = hashingService.PasswordHashing(userDetailDTO.Password, salt);

            UserLogin userLogin = new()
            {
                Username = userDetailDTO.UserName,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsActive = true,
                UserDetail = new()
                {
                    FirstName = userDetailDTO.FirstName,
                    LastName = userDetailDTO.LastName,
                }
            };

            return userLogin;
        }
    }
}
