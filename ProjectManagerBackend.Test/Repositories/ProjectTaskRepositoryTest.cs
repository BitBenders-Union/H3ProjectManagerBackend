
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ProjectManagerBackend.Test.Repositories
{
    public  class ProjectTaskRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;

        GenericRepository<ProjectTask> _repository;
        public ProjectTaskRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "MockProjectTask").Options;

            _context = new DataContext(options);
            _context.Database.EnsureDeleted();

            _context.ProjectTasks.Add(new ProjectTask { Id = 1, Name = "Test ProjectTask 1", Description = "Test Description 1" });
            _context.ProjectTasks.Add(new ProjectTask { Id = 2, Name = "Test ProjectTask 2", Description = "Test Description 2" });
            _context.ProjectTasks.Add(new ProjectTask { Id = 3, Name = "Test ProjectTask 3", Description = "Test Description 3" });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllProjectTask_ReturnList()
        {
            // Arrange
            _repository = new(_context);

            // Act
            var returnedList = _repository.GetAllAsync();
            returnedList.Wait();
            var newList = returnedList.Result.ToList();

            // Assert
            Assert.Equal(3, newList.Count);

        }

        [Fact]
        public async Task GetProjectTaskById_ReturnOneProjectTask()
        {
            // Arrange
            _repository = new(_context);

            // Act
            ProjectTask projectTask = await _repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, projectTask.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await _repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateProjectTask_ReturnProjectTask()
        {
            ProjectTask projectTask = new ProjectTask { Name = "Test ProjectTask 50", Description = "Test Description 50" };

            // Arrange
          _repository = new(_context);

            // Act
            ProjectTask returnProjectTask = await _repository.CreateAsync(projectTask);

            // Assert
            Assert.Equal(returnProjectTask, projectTask);
        }

        [Fact]
        public async Task DeleteProjectTask_ReturnTrue()
        {
            ProjectTask projectTask = new ProjectTask { Name = "Test ProjectTask 999", Description = "Test Description 999" };
            _context.ProjectTasks.Add(projectTask);
            _context.SaveChanges();

            // Arrange
          _repository = new(_context);

            // Act
            bool result = await _repository.DeleteAsync(projectTask.Id);
            bool falseResult = await _repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateProjectTask_ReturnOkResult()
        {
            // Arrange
          _repository = new(_context);

            ProjectTask projectTask = await _repository.GetByIdAsync(1);
            projectTask.Name = "Test Location 1 updated";

            // Act
            var result = await _repository.UpdateAsync(projectTask);

            //Assert
            Assert.True(result);
        }
    }
}
