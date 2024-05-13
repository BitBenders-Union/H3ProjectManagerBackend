

using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.Models;
using ProjectManagerBackend.Repo.Repositories;

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
            var projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.GetAllCategories();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetCategoryById_ReturnCategory()
        {
            // Arrange
            var projectCategoryRepository = new ProjectCategoryRepository(_context);
            // Act
            var result = await projectCategoryRepository.GetCategoryById(1);
            // Assert
            Assert.Equal("Test Category", result.Name);
        }

        [Fact]
        public async Task CreateCategori_ReturnCategori()
        {
            // Arrange
            var projectCategoryRepository = new ProjectCategoryRepository(_context);
            // Act
            var result = await projectCategoryRepository.CreateCategory(new ProjectCategory { Name = "Test Category 4" });
            // Assert
            Assert.Equal("Test Category 4", result.Name);
        }

        [Fact]
        public async Task UpdateCategory_ReturnTrue()
        {
            // Arrange
            var projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.UpdateCategory(new ProjectCategory { Name = "Test Category Updated" });

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnTrue()
        {
            // Arrange
            var projectCategoryRepository = new ProjectCategoryRepository(_context);

            // Act
            var result = await projectCategoryRepository.DeleteCategory(1);

            // Assert
            Assert.True(result);
        }
    }
}
