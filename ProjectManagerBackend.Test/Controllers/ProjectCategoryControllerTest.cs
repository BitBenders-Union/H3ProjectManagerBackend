
namespace ProjectManagerBackend.Test.Controllers
{
    public class ProjectCategoryControllerTest
    {
        private readonly IProjectCategory _categoryRepository;
        private readonly IMappingService _mapping;

        private readonly List<ProjectCategory> _projectCategoriesList;
        private readonly ProjectCategory _singleProjectCategory;
        private readonly ProjectCategoryDTO _singleProjectCategoryDTO;

        public ProjectCategoryControllerTest()
        {
            // Setup mocks
            _categoryRepository = new Mock<IProjectCategory>().Object;
            _mapping = new Mock<IMappingService>().Object;

            _projectCategoriesList = new List<ProjectCategory>
            {
                new ProjectCategory { Id = 1, Name = "ProjectCategory 1" },
                new ProjectCategory { Id = 2, Name = "ProjectCategory 2" },
                new ProjectCategory { Id = 3, Name = "ProjectCategory 3" }
            };

            _singleProjectCategory = new ProjectCategory { Id = 1, Name = "ProjectCategory 1" };

            _singleProjectCategoryDTO = new ProjectCategoryDTO { Name = "ProjectCategory 1" };
        }

        [Fact]
        public async Task GetAllProjectCategories_ReturnOkResultAndProjectCategoriesList()
        {
            // Arrange            
            // Set up the mock repository to return the 'projectCategories' list when GetAllAsync is called
            Mock.Get(_categoryRepository).Setup(repo => repo.GetAllCategories()).ReturnsAsync(_projectCategoriesList);

            // Create instance of controller with mocked dependencies
            var controller = new ProjectCategoryController(_categoryRepository, _mapping);

            // Act
            var result = await controller.GetAll();

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Check if the value of the OkObjectResult is of type IEnumerable<ProjectCategoryDTO>
            var model = Assert.IsAssignableFrom<List<ProjectCategory>>(okResult.Value);

            // Check if the returned collection is not empty
            Assert.NotEmpty(model);

            // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetProjectCategoryById_ReturnOkResult()
        {
            // Arrange
            Mock.Get(_categoryRepository)
                .Setup(repo => repo.GetCategoryById(1)) // Existing setup for GetCategoryById
                .ReturnsAsync(_singleProjectCategory);

            Mock.Get(_categoryRepository)
                .Setup(repo => repo.DoesExist(1)) // Setup for DoesExist
                .ReturnsAsync(true); // Return true to simulate existence


            // Create instance of controller with mocked dependencies
            var controller = new ProjectCategoryController(_categoryRepository, _mapping);

            // Act
            var result = await controller.GetById(1); // Call GetById method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200
        }

        [Fact]
        public async Task CreateProjectCategory_ReturnOkResult()
        {
            // Arrange
            Mock.Get(_categoryRepository)
                .Setup(repo => repo.CreateCategory(It.IsAny<ProjectCategory>())) // Mock CreateCategory with any argument
                .ReturnsAsync(_singleProjectCategory); // Mocks async behavior, returns expected category


            // Create instance of controller with mocked dependencies
            var controller = new ProjectCategoryController(_categoryRepository, _mapping);

            // Act
            var result = await controller.Create(_singleProjectCategoryDTO); // Call Create method with _singleProjectCategoryDTO as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200
        }

        [Fact]
        public async Task DeleteProjectCategory_ReturnOkResult()
        {
            // Arrange
            // Setup the mock repository to return true when DoesExist is called with 1 as parameter
            Mock.Get(_categoryRepository).Setup(repo => repo.DoesExist(1)).ReturnsAsync(true);

            // Setup the mock repository to return true when DeleteCategory is called with 1 as parameter
            Mock.Get(_categoryRepository).Setup(repo => repo.DeleteCategory(1)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new ProjectCategoryController(_categoryRepository, _mapping);

            // Act
            var result = await controller.Delete(1); // Call Delete method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200
        }

        [Fact]
        public async Task UpdateProjectCategory_ReturnOkResult()
        {
            // Arrange
            // Setup the mock repository to return true when DoesExist is called with 0 as parameter
            Mock.Get(_categoryRepository).Setup(repo => repo.DoesExist(0)).ReturnsAsync(true);

            // Setup the mock repository to return true when UpdateCategory is called with _singleProjectCategory as parameter
            Mock.Get(_categoryRepository).Setup(repo => repo.UpdateCategory(_singleProjectCategory)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new ProjectCategoryController(_categoryRepository, _mapping);

            // Act
            var result = await controller.Update(_singleProjectCategoryDTO); // Call Update method with _singleProjectCategoryDTO as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200
        }

    }
}
