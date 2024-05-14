
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.Test.Repositories
{
    public class LocationRepositoryTest
    {

        DbContextOptions<DataContext> options;

        DataContext _context;

        public LocationRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockLocation").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Locations.Add(new Location { Id = 1, Name = "Test Location 1", Address = "Test Address 1" });
            _context.Locations.Add(new Location { Id = 2, Name = "Test Location 2", Address = "Test Address 2" });
            _context.Locations.Add(new Location { Id = 3, Name = "Test Location 3", Address = "Test Address 3" });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllLocations_ReturnList()
        {
            // Arrange
            GenericRepository<Location> repository = new(_context);

            // Act
            ICollection<Location> locationList = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, locationList.Count);
        }
        
        [Fact]
        public async Task GetLocationById_ReturnOneLocation()
        {
            // Arrange
            GenericRepository<Location> repository = new(_context);

            // Act
            Location location = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, location.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));    
        }

        [Fact]
        public async Task CreateLocation_ReturnLocation()
        {
            Location location = new Location { Id = 50, Name = "Test Location 50", Address = "Test Address 50" };

            // Arrange
            GenericRepository<Location> repository = new(_context);

            // Act
            Location returnLocation = await repository.CreateAsync(location);

            // Assert
            Assert.Equal(returnLocation, location);
        }

        [Fact]
        public async Task DeleteLocation_ReturnTrue()
        {
            Location location = new() { Name = "Test Location 999", Address = "Test Address 999" };
            _context.Locations.Add(location);
            _context.SaveChanges();

            // Arrange
            GenericRepository<Location> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(location.Id); 
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist
            
            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateLocation_ReturnOkResult()
        {
            // Arrange
            GenericRepository<Location> repository = new(_context);

            Location location = await repository.GetByIdAsync(1);
            location.Name = "Test Location 1 updated";

            // Act
            var result = await repository.UpdateAsync(location);

            //Assert
            Assert.True(result);
        }

    }
}
