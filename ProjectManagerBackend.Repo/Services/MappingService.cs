using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using System.Collections;


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

        //public TMapped? Map<T, TMapped>(T source)
        //{
        //    if (source == null)
        //        return default;

        //    // since we don't know the type of the source, we can't use the new keyword.
        //    // therefore we are forced to use the Activator.CreateInstance<T>(); to create an object of our model
        //    // look at link, for an explanation.
        //    // https://stackoverflow.com/questions/1649066/activator-createinstancet-vs-new
        //    var destination = Activator.CreateInstance<TMapped>();

        //    // get the properties of the entities
        //    var sourceProperties = typeof(T).GetProperties();
        //    var destinationProperties = typeof(TMapped).GetProperties();


        //    // loop over the properties in the source
        //    foreach (var sourceProperty in sourceProperties)
        //    {
        //        // find the destination property that has a matching name with the source property
        //        var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

        //        // if matching property exist make sure it is the same type, and that it is writeable
        //        if (destinationProperty != null && destinationProperty.CanWrite && destinationProperty.PropertyType == sourceProperty.PropertyType)
        //        {
        //            // map the property from source to destination
        //            // source is input, destination = output
        //            destinationProperty.SetValue(destination, sourceProperty.GetValue(source));

        //            // the above line modifies the destination object
        //        }
        //        // if the destination property is the same name but not the same type, we need to map the property with a recursive loop using the type of the soruce property and destination property
        //        // but only if the properties are a class, and not a primitive type
        //        else if (destinationProperty != null && destinationProperty.CanWrite && sourceProperty.PropertyType.IsClass && destinationProperty.PropertyType.IsClass)
        //        {
        //            // get the value of the source property
        //            var sourcePropertyValue = sourceProperty.GetValue(source);

        //            // if the source property value is null, we can't map it, so we skip it
        //            if (sourcePropertyValue == null)
        //                continue;

        //            // get the value of the destination property
        //            var destinationPropertyValue = destinationProperty.GetValue(destination);

        //            // if the destination property value is null, we can't map it, so we skip it
        //            if (destinationPropertyValue == null)
        //                continue;

        //            // map the property with a recursive loop
        //            // since the Map<> method is generic, we need to specify the type of the source property and destination property
        //            // we do this by using the MakeGenericMethod method, and passing the type of the source property and destination property
        //            // we then invoke the method, and pass the source property value as the parameter
        //            // the return value is then casted to the destination property type, and set as the value of the destination property
        //            destinationProperty.SetValue(destination, typeof(MappingService).GetMethod("Map").MakeGenericMethod(sourceProperty.PropertyType, destinationProperty.PropertyType).Invoke(this, new object[] { sourcePropertyValue }));


        //        }

        //    }

        //    // return mapped object
        //    return destination;


        //}

        public TMapped? Map<T, TMapped>(T source)
        {
            if (source == null)
                return default;

            var destination = Activator.CreateInstance<TMapped>();

            var sourceProperties = typeof(T).GetProperties();
            var destinationProperties = typeof(TMapped).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.CanWrite && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
                else if (destinationProperty != null && destinationProperty.CanWrite && sourceProperty.PropertyType.IsClass && destinationProperty.PropertyType.IsClass)
                {
                    var sourcePropertyValue = sourceProperty.GetValue(source);

                    if (sourcePropertyValue == null)
                        continue;

                    var destinationPropertyValue = destinationProperty.GetValue(destination);

                    if (destinationPropertyValue == null)
                    {
                        // Create an instance of the destination property type
                        destinationPropertyValue = Activator.CreateInstance(destinationProperty.PropertyType);
                        destinationProperty.SetValue(destination, destinationPropertyValue);
                    }

                    // Recursive call to Map method
                    destinationProperty.SetValue(destination,
                        typeof(MappingService)
                            .GetMethod("Map")
                            .MakeGenericMethod(sourceProperty.PropertyType, destinationProperty.PropertyType)
                            .Invoke(this, new object[] { sourcePropertyValue }));


                }
            }

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
