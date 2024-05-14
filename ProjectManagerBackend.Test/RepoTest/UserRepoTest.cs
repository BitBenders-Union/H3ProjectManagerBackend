using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.Models;
using ProjectManagerBackend.Repo.Repositories;
using ProjectManagerBackend.Repo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.RepoTest
{
    public class UserRepoTest
    {
        DbContextOptions<DataContext> options;
        DataContext context;
        HashingService hashingService;

        public UserRepoTest()
        {
            options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "ThisBase").Options;
            context = new DataContext(options);
            context.Database.EnsureDeleted();
            hashingService = new HashingService();
            byte[] salt = hashingService.GenerateSalt();

            UserDetail u1 = new UserDetail() { 
                Id = 1,
                Username = "Test1",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("FUCK", salt),
                IsActive = true,
                FirstName = "TestFN",
                LastName = "TestLN",
                CreatedDate = DateTime.Now
            };
            UserDetail u2 = new UserDetail() { 
                Id = 2,
                Username = "Test2",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("FUCK", salt),
                IsActive = true,
                FirstName = "Test2FN",
                LastName = "Test2LN",
                CreatedDate = DateTime.Now
            };
            UserDetail u3 = new UserDetail() { 
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

        //[Fact]
        //public async void GetNotExistUserId_ReturnNull()
        //{
        //    GenericRepository<UserDetail> repo = new GenericRepository<UserDetail>(context);
        //    var result = await repo.GetByIdAsync(199);
        //    var ex = Assert.Throws(() =>
        //    {
        //        throw new Exception(new Exception());
        //    });

        //    Assert.Equal("No Entity witn Id Found", Exception);
        //}

    }
}
