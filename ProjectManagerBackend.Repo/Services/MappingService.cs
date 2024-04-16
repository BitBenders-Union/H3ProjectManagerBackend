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
        public UserDetail AddUser(UserDetailDTO userDetailDTO)
        {
            byte[] salt = hashingService.GenerateSalt();
            byte[] hash = hashingService.PasswordHashing(userDetailDTO.Password, salt);

            UserDetail userDetail = new()
            {
                Username = userDetailDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsActive = true,
                FirstName = userDetailDTO.FirstName,
                LastName = userDetailDTO.LastName,
                CreatedDate = DateTime.Now
            };

            return userDetail;
        }

        public TMapped Map<T, TMapped>(T source)
        {
            if (source == null)
                return default(TMapped);

            var destination = Activator.CreateInstance<TMapped>();
            var sourceProperties = typeof(T).GetProperties();
            var destinationProperties = typeof(TMapped).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }

            return destination;


        }

    }
}
