
namespace ProjectManagerBackend.Test.Repositories
{
    public class ProjectStatusRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;

        public ProjectStatusRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockStatus").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.ProjectStatus.Add(new ProjectStatus { Id = 1, Name = "Test Location 1"  });
            _context.ProjectStatus.Add(new ProjectStatus { Id = 2, Name = "Test Location 2"  });
            _context.ProjectStatus.Add(new ProjectStatus { Id = 3, Name = "Test Location 3"  });
            

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllProjectStatus_ReturnList()
        {
            // Arrange
            GenericRepository<ProjectStatus> repository = new(_context);

            // Act
            ICollection<ProjectStatus> statusList = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, statusList.Count);
        }

        [Fact]
        public async Task GetProjectStatusById_ReturnOneProjectStatus()
        {
            // Arrange
            GenericRepository<ProjectStatus> repository = new(_context);

            // Act
            ProjectStatus projectStatus = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, projectStatus.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateProjectStatus_ReturnProjectStatus()
        {
            ProjectStatus projectStatus = new ProjectStatus { Id = 50, Name = "Test ProjectStatus 50" };

            // Arrange
            GenericRepository<ProjectStatus> repository = new(_context);

            // Act
            ProjectStatus returnProjectStatus = await repository.CreateAsync(projectStatus);

            // Assert
            Assert.Equal(returnProjectStatus, projectStatus);
        }

        [Fact]
        public async Task DeleteProjectStatus_ReturnTrue()
        {
            ProjectStatus projectStatus = new ProjectStatus { Name = "Test ProjectStatus 999" };
            _context.ProjectStatus.Add(projectStatus);
            _context.SaveChanges();

            // Arrange
            GenericRepository<ProjectStatus> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(projectStatus.Id); // Assuming Id 1 does exist 
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateProjectStatus_ReturnOkResult()
        {
            // Arrange
            GenericRepository<ProjectStatus> repository = new(_context);

            ProjectStatus projectStatus = await repository.GetByIdAsync(1);
            projectStatus.Name = "Test ProjectStatus 1 updated";

            // Act
            var result = await repository.UpdateAsync(projectStatus);

            //Assert
            Assert.True(result);
        }

    }
}

