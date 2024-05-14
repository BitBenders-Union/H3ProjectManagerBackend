
namespace ProjectManagerBackend.Test.Repositories
{
    public class ProjectTaskCategoryRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;
        public ProjectTaskCategoryRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockTaskCategory").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.ProjectTaskCategories.AddAsync(new ProjectTaskCategory { Id = 1, Name = "Test ProjectTaskCategory 1" });
            _context.ProjectTaskCategories.AddAsync(new ProjectTaskCategory { Id = 2, Name = "Test ProjectTaskCategory 2" });
            _context.ProjectTaskCategories.AddAsync(new ProjectTaskCategory { Id = 3, Name = "Test ProjectTaskCategory 3" });
            

            _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllProjectTaskCategories_ReturnList()
        {
            // Arrange
            GenericRepository<ProjectTaskCategory> repository = new(_context);

            // Act
            ICollection<ProjectTaskCategory> returnedList = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, returnedList.Count);
        }

        [Fact]
        public async Task GetProjectTaskCategory_ReturnOneProjectTaskCategory()
        {
            // Arrange
            GenericRepository<ProjectTaskCategory> repository = new(_context);

            // Act
            ProjectTaskCategory taskCategory = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, taskCategory.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateProjectTaskCategory_ReturnProjectTaskCategory()
        {
            ProjectTaskCategory taskCategory = new ProjectTaskCategory { Id = 50, Name = "Test ProjectTaskCategory 50" };

            // Arrange
            GenericRepository<ProjectTaskCategory> repository = new(_context);

            // Act
            ProjectTaskCategory returnedCategory = await repository.CreateAsync(taskCategory);

            // Assert
            Assert.Equal(returnedCategory, taskCategory);
        }

        [Fact]
        public async Task DeleteProjectTaskCategory_ReturnTrue()
        {
            ProjectTaskCategory taskCategory = new ProjectTaskCategory {  Name = "Test ProjectTaskCategory 999" };
            _context.ProjectTaskCategories.Add(taskCategory);
            _context.SaveChanges();

            // Arrange
            GenericRepository<ProjectTaskCategory> repository = new(_context);

            // Act


            bool result = await repository.DeleteAsync(taskCategory.Id); // Assuming Id 1 does exist 
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateProjectTaskCategory_ReturnOkResult()
        {
            // Arrange
            GenericRepository<ProjectTaskCategory> repository = new(_context);

            ProjectTaskCategory projectTaskCategory = await repository.GetByIdAsync(1);
            projectTaskCategory.Name = "Test Location 1 updated";

            // Act
            var result = await repository.UpdateAsync(projectTaskCategory);

            //Assert
            Assert.True(result);
        }

    }
}