
namespace ProjectManagerBackend.Test.Repositories
{
    public class PriorityRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;

        public PriorityRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockPriority").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Priorities.Add(new Priority { Id = 1, Name = "Test Priority 1", Level = 0 });
            _context.Priorities.Add(new Priority { Id = 2, Name = "Test Priority 2", Level = 1 });
            _context.Priorities.Add(new Priority { Id = 3, Name = "Test Priority 3", Level = 2 });           

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllPriorities_ReturnList()
        {
            // Arrange
            GenericRepository<Priority> repository = new(_context);

            // Act
            ICollection<Priority> priorityList = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, priorityList.Count);
        }

        [Fact]
        public async Task GetPriorityById_ReturnOnePriority()
        {
            // Arrange
            GenericRepository<Priority> repository = new(_context);

            // Act
            Priority priority = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, priority.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreatePriority_ReturnPriority()
        {
            Priority priority = new Priority { Id = 50, Name = "Test Priority 50", Level = 50 };

            // Arrange
            GenericRepository<Priority> repository = new(_context);

            // Act
            Priority returnPriority = await repository.CreateAsync(priority);

            // Assert
            Assert.Equal(returnPriority, priority);
        }

        [Fact]
        public async Task DeletePriority_ReturnTrue()
        {
            Priority priority = new Priority { Name = "Test Priority 999", Level = 999 };
            _context.Priorities.Add(priority);
            _context.SaveChanges();

            // Arrange
            GenericRepository<Priority> repository = new(_context);

            // Act

            bool result = await repository.DeleteAsync(priority.Id);  
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdatePriority_ReturnOkResult()
        {
            // Arrange
            GenericRepository<Priority> repository = new(_context);

            Priority priority = await repository.GetByIdAsync(1);
            priority.Name = "Test Location 1 updated";

            // Act
            var result = await repository.UpdateAsync(priority);

            //Assert
            Assert.True(result);
        }

    }
}
