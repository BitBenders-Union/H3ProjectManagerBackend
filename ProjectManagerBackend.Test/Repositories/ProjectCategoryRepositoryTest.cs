
namespace ProjectManagerBackend.Test.Repositories
{
    public class ProjectCategoryRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;

        public ProjectCategoryRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockProjectCategory").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.ProjectCategories.Add(new ProjectCategory { Id = 1, Name = "Test Category" });
            _context.ProjectCategories.Add(new ProjectCategory { Id = 2, Name = "Test Category 2" });
            _context.ProjectCategories.Add(new ProjectCategory { Id = 3, Name = "Test Category 3" });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllCategories_ReturnList()
        {
            // Arrange
            ProjectCategoryRepository projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.GetAllCategories();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetCategoryById_ReturnCategory()
        {
            // Arrange
            ProjectCategoryRepository projectCategoryRepository = new ProjectCategoryRepository(_context);
            // Act
            var result = await projectCategoryRepository.GetCategoryById(1);
            // Assert
            Assert.Equal("Test Category", result.Name);
        }

        [Fact]
        public async Task CreateCategori_ReturnCategori()
        {
            ProjectCategory newCategory = new ProjectCategory { Name = "Test Category 4" };

            // Arrange
            ProjectCategoryRepository projectCategoryRepository = new ProjectCategoryRepository(_context);
            // Act

            ProjectCategory result = await projectCategoryRepository.CreateCategory(newCategory);
            // Assert

            Assert.Equal(newCategory, result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnTrue()
        {
            // Arrange
            ProjectCategoryRepository projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.UpdateCategory(new ProjectCategory { Name = "Test Category Updated" });

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnTrueIfDeleted()
        {
            // Arrange
            ProjectCategoryRepository projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.DeleteCategory(1);
            var result2 = await projectCategoryRepository.DeleteCategory(21);

            // Assert
            Assert.True(result);
            Assert.False(result2);
        }

        [Fact]
        public async Task DeleteCategory_PassAsFalseIfReturnIsFalse()
        {
            // Arrange
            ProjectCategoryRepository projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.DeleteCategory(21);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DoesExist_ReturnTrue()
        {
            // Arrange
            var projectCategoriRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoriRepository.DoesExist(1);

            // Assert
            Assert.True(result);
        }
    }
}
