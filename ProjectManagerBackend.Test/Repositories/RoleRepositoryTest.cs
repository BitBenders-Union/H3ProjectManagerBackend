using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.Repositories
{
    public class RoleRepositoryTest
    {

        DbContextOptions<DataContext> options;

        DataContext _context;


        public RoleRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockRole").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Add(new Role { Id = 1, Name = "Test Role 1", Description = "Test Role Description 1", IsActive = true });
            _context.Add(new Role { Id = 2, Name = "Test Role 2", Description = "Test Role Description 2", IsActive = true });
            _context.Add(new Role { Id = 3, Name = "Test Role 3", Description = "Test Role Description 3", IsActive = true });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllRoles_ReturnList()
        {
            // Arrange
            GenericRepository<Role> repository = new(_context);


            var returnedList = repository.GetAllAsync();
            returnedList.Wait();

            var newList = returnedList.Result.ToList();


            // Assert
            Assert.Equal(3, newList.Count);

        }

        [Fact]
        public async Task GetRoleById_ReturnOneRole()
        {
            // Arrange
            GenericRepository<Role> repository = new(_context);

            // Act
            Role role = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, role.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateRole_ReturnRole()
        {
           Role role = new Role { Name = "Test Role 50", Description = "Test Role Description 50", IsActive = true };

            // Arrange
            GenericRepository<Role> repository = new(_context);

            // Act
            Role returnRole = await repository.CreateAsync(role);

            // Assert
            Assert.Equal(returnRole, role);
        }

        [Fact]
        public async Task DeleteRole_ReturnTrue()
        {
            Role role = new Role { Name = "Test Role 999", Description = "Test Role Description 999", IsActive = true };
            _context.Roles.Add(role);
            _context.SaveChanges();

            // Arrange
            GenericRepository<Role> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(role.Id);
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateRole_ReturnOkResult()
        {
            // Arrange
            GenericRepository<Role> repository = new(_context);

            Role role = await repository.GetByIdAsync(1);
            role.Name = "Test Location 1 updated";

            // Act
            var result = await repository.UpdateAsync(role);

            //Assert
            Assert.True(result);
        }

    }
}
