
namespace ProjectManagerBackend.Test.Controllers
{
    public class ProjectTaskCategoryControllerTest
    {
        private readonly IGenericRepository<ProjectTaskCategory> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<ProjectTaskCategory, ProjectTaskCategoryDTO, ProjectTaskCategoryDTO> _controller;

        private readonly List<ProjectTaskCategory> _projectTaskCategoriesList;
        private readonly ProjectTaskCategory _singleProjectTaskCategory;
        private readonly ProjectTaskCategoryDTO _singleProjectTaskCategoryDTO;

        public ProjectTaskCategoryControllerTest()
        {
            // Setup mocks
            _repository = new Mock<IGenericRepository<ProjectTaskCategory>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<ProjectTaskCategory, ProjectTaskCategoryDTO, ProjectTaskCategoryDTO>(_repository, _mapping, _validationService);

            // Setup data
            _projectTaskCategoriesList = new List<ProjectTaskCategory>
            {
                new ProjectTaskCategory { Id = 1, Name = "ProjectTaskCategory 1" },
                new ProjectTaskCategory { Id = 2, Name = "ProjectTaskCategory 2" },
                new ProjectTaskCategory { Id = 3, Name = "ProjectTaskCategory 3" }
            };

            _singleProjectTaskCategory = new ProjectTaskCategory { Id = 1, Name = "ProjectTaskCategory 1" };

            _singleProjectTaskCategoryDTO = new ProjectTaskCategoryDTO { Name = "ProjectTaskCategory 1" };            
        }

        [Fact]
        public async Task GetAllPrProjectTaskCategories_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projectTaskCategories' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_projectTaskCategoriesList);

            // Act
            var result = await _controller.GetAll(); // Call the GetAll method of the controller

            // Assert
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is of type OkObjectResult

            var model = Assert.IsAssignableFrom<IEnumerable<ProjectTaskCategoryDTO>>(okResult.Value); // Check if the value of the OkObjectResult is of type IEnumerable<ProjectTaskCategoryDTO>

            Assert.NotEmpty(model); // Check if the returned collection is not empty
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }
    }
}
