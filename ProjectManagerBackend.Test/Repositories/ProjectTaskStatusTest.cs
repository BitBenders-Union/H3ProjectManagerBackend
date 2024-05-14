using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.Repositories
{
    public class ProjectTaskStatusTest
    {

        DbContextOptions<DataContext> options;

        DataContext _context;


        public ProjectTaskStatusTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockTaskStatus").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.ProjectTaskStatus.Add(new ProjectTaskStatus { Id = 1, Name = "Test ProjectTaskStatus 1" });
            _context.ProjectTaskStatus.Add(new ProjectTaskStatus { Id = 2, Name = "Test ProjectTaskStatus 2" });
            _context.ProjectTaskStatus.Add(new ProjectTaskStatus { Id = 3, Name = "Test ProjectTaskStatus 3" });

            if (_context.SaveChanges() < 1)
            {
                throw new Exception("Could not seed data");
            }

        }

        [Fact]
        public async Task GetAllProjectTaskStatus_ReturnList()
        {
            // Arrange
            GenericRepository<ProjectTaskStatus> repository = new(_context);


            var returnedList = repository.GetAllAsync();
            returnedList.Wait();

            var newList = returnedList.Result.ToList();


            // Assert
            Assert.Equal(3, newList.Count);

        }

        [Fact]
        public async Task GetProjectTaskStatusById_ReturnOneProjectTaskStatus()
        {
            // Arrange
            GenericRepository<ProjectTaskStatus> repository = new(_context);

            // Act
            ProjectTaskStatus projectTaskStatus = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, projectTaskStatus.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateProjectTaskStatus_ReturnProjectTaskStatus()
        {
            ProjectTaskStatus projectTaskStatus = new ProjectTaskStatus { Id = 50, Name = "Test ProjectTaskStatus 50" };

            // Arrange
            GenericRepository<ProjectTaskStatus> repository = new(_context);

            // Act
            ProjectTaskStatus returnProjectTaskStatus = await repository.CreateAsync(projectTaskStatus);

            // Assert
            Assert.Equal(returnProjectTaskStatus, projectTaskStatus);
        }

        [Fact]
        public async Task DeleteProjectTaskStatus_ReturnTrue()
        {
            ProjectTaskStatus projectTaskStatus = new ProjectTaskStatus { Name = "Test ProjectTaskStatus 999" };
            _context.ProjectTaskStatus.Add(projectTaskStatus);
            _context.SaveChanges();

            // Arrange
            GenericRepository<ProjectTaskStatus> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(projectTaskStatus.Id);
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateProjectTaskStatus_ReturnOkResult()
        {
            // Arrange
            GenericRepository<ProjectTaskStatus> repository = new(_context);

            ProjectTaskStatus projectTaskStatus = await repository.GetByIdAsync(1);
            projectTaskStatus.Name = "Test Location 1 updated";

            // Act
            var result = await repository.UpdateAsync(projectTaskStatus);

            //Assert
            Assert.True(result);
        }

    }
}
