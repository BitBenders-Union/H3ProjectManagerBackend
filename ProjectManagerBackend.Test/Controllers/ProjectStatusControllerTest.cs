
namespace ProjectManagerBackend.Test.Controllers
{
    public class ProjectStatusControllerTest
    {
        private readonly IGenericRepository<ProjectStatus> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<ProjectStatus, ProjectStatusDTO, ProjectStatusDTO> _controller;

        private readonly List<ProjectStatus> _projectStatusList;
        private readonly ProjectStatus _singleProjectStatus;
        private readonly ProjectStatusDTO _singleProjectStatusDTO;

        public ProjectStatusControllerTest()
        {
            // Setup mocks
            _repository = new Mock<IGenericRepository<ProjectStatus>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            _controller = new GenericController<ProjectStatus, ProjectStatusDTO, ProjectStatusDTO>(_repository, _mapping, _validationService);

            _projectStatusList = new List<ProjectStatus>
            {
                new ProjectStatus { Id = 1, Name = "ProjectStatus 1" },
                new ProjectStatus { Id = 2, Name = "ProjectStatus 2" },
                new ProjectStatus { Id = 3, Name = "ProjectStatus 3" }
            };

            _singleProjectStatus = new ProjectStatus { Id = 1, Name = "ProjectStatus 1" };

            _singleProjectStatusDTO = new ProjectStatusDTO { Name = "ProjectStatus 1" };
        }

        [Fact]
        public async Task GetAllProjectStatuses_ReturnsOkResultAndProjectStatusesList()
        {
            // Arrange            
            // Set up the mock repository to return the 'projectStatuses' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_projectStatusList);
                        
            // Act
            var result = await _controller.GetAll();

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if the value of the OkObjectResult is of type IEnumerable<ProjectStatusDTOResponse>
            var model = Assert.IsAssignableFrom<IEnumerable<ProjectStatusDTO>>(okResult.Value);

            // Check if the returned collection is not empty
            Assert.NotEmpty(model);

            // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetProjectStatusById_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projectStatus' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProjectStatus);

            // Act
            var result = await _controller.GetById(1); // GetByIdAsync is called with 1

            // Assert
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task CreateProjectStatus_ReturnOkResult()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService).Setup(service => service.WhiteSpaceValidation(_singleProjectStatusDTO)).Returns(true);

            // Act
            var result = await _controller.Create(_singleProjectStatusDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task DeleteProjectStatus_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projectStatus' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProjectStatus);
           
            // Act
            var result = await _controller.Delete(1); // Delete method is called with 1

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task UpdateProjectStatus_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projectStatus' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.UpdateAsync(_singleProjectStatus)).ReturnsAsync(true);

            // Controller instance
            var controller = new ProjectStatusController(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Update(_singleProjectStatusDTO); // Call Update method with _singleProjectStatusDTO

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }


    }
}
