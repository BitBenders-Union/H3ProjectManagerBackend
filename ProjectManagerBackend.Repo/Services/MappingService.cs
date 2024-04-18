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

        public UserDetail UserToken(TokenDTO tokenDTO)
        {
            UserDetail userToken = new()
            {
                Token = tokenDTO.AccessToken,
                RefreshToken = tokenDTO.RefreshToken
            };

            return userToken;
        }

        public TMapped? Map<T, TMapped>(T source)
        {
            if (source == null)
                return default;

            // since we don't know the type of the source, we can't use the new keyword.
            // therefore we are forced to use the Activator.CreateInstance<T>(); to create an object of our model
            // look at link, for an explanation.
            // https://stackoverflow.com/questions/1649066/activator-createinstancet-vs-new
            var destination = Activator.CreateInstance<TMapped>();

            // get the properties of the entities
            var sourceProperties = typeof(T).GetProperties();
            var destinationProperties = typeof(TMapped).GetProperties();


            // loop over the properties in the source
            foreach (var sourceProperty in sourceProperties)
            {
                // find matching property
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

                // if matching property exist
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // map the property from source to destination
                    // source is input, destination = output
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }

            // return mapped object
            return destination;


        }

        public UserDetail UserLogin(LoginDTO loginDTO)
        {
            UserDetail userDetail = new()
            {
                Username = loginDTO.Username,
            };
            return userDetail;
        }
    }
}
