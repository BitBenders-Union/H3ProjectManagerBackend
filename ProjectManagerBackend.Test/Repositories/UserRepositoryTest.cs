using ProjectManagerBackend.Repo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.Repositories
{
    public class UserRepositoryTest
    {
        DbContextOptions<DataContext> options;
        DataContext context;
        HashingService hashingService;

        public UserRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "ThisBase").Options;
            context = new DataContext(options);
            context.Database.EnsureDeleted();
            hashingService = new HashingService();
            byte[] salt = hashingService.GenerateSalt();

            UserDetail u1 = new UserDetail()
            {
                Id = 1,
                Username = "Test1",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("FUCK", salt),
                IsActive = true,
                FirstName = "TestFN",
                LastName = "TestLN",
                CreatedDate = DateTime.Now
            };
            UserDetail u2 = new UserDetail()
            {
                Id = 2,
                Username = "Test2",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("FUCK", salt),
                IsActive = true,
                FirstName = "Test2FN",
                LastName = "Test2LN",
                CreatedDate = DateTime.Now
            };
            UserDetail u3 = new UserDetail()
            {
                Id = 3,
                Username = "Test3",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("FUCK", salt),
                IsActive = true,
                FirstName = "Test3FN",
                LastName = "Test3LN",
                CreatedDate = DateTime.Now
            };

            context.Add(u1);
            context.Add(u2);
            context.Add(u3);
            context.SaveChanges();
        }

        [Fact]
        public async void GetAllUsers_ReturnAll()
        {
            GenericRepository<UserDetail> repo = new GenericRepository<UserDetail>(context);
            var result = await repo.GetAllAsync();
            var expected = 3;

            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async void GetUserById_ReturnExist()
        {
            GenericRepository<UserDetail> repo = new GenericRepository<UserDetail>(context);
            var result = await repo.GetByIdAsync(2);

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetNotExistUserId_ThrowsException()
        {
            GenericRepository<UserDetail> repo = new GenericRepository<UserDetail>(context);
            await Assert.ThrowsAsync<Exception>(async () => await repo.GetByIdAsync(99));
        }

        [Fact]
        public async void CreateUser_UserDetails()
        {
            byte[] salt = hashingService.GenerateSalt();
            UserDetail u4 = new UserDetail()
            {
                Username = "Test4",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("FUCK", salt),
                IsActive = true,
                FirstName = "Test4FN",
                LastName = "Test4LN",
                CreatedDate = DateTime.Now
            };

            UserRepository userRepository = new UserRepository(context);
            var result = await userRepository.CreateUserAsync(u4);
            Assert.NotNull(result);
            Assert.Equal(result, u4);
        }

        [Fact]
        public async void UpdateUser_ReturnTrue()
        {
            GenericRepository<UserDetail> repo = new GenericRepository<UserDetail>(context);
            UserDetail u1 = await repo.GetByIdAsync(2);
            u1.FirstName = "TestUpdatedFN";

            var result = await repo.UpdateAsync(u1);
            Assert.True(result);
        }

        [Fact]
        public async void DeleteUser_ReturnTrue()
        {
            GenericRepository<UserDetail> repo = new GenericRepository<UserDetail>(context);
            bool result = await repo.DeleteAsync(2);
            bool falseResult = await repo.DeleteAsync(99);

            Assert.True(result);
            Assert.False(falseResult);
        }
    }
}
