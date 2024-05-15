

namespace ProjectManagerBackend.Test.Controllers
{
    public class ProjectTaskControllerTest
    {
        private readonly IGenericRepository<ProjectTask> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<ProjectTask, ProjectTaskDTO, ProjectTaskDTO> _controller;

        private readonly List<ProjectTask> _projectTasksList;
        private readonly ProjectTask _singleProjectTask;
        private readonly ProjectTaskDTO _singleProjectTaskDTO;

        public ProjectTaskControllerTest()
        {
            // Setup mocks
            _repository = new Mock<IGenericRepository<ProjectTask>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<ProjectTask, ProjectTaskDTO, ProjectTaskDTO>(_repository, _mapping, _validationService);

            // Setup data
            _projectTasksList = new List<ProjectTask>
            {
                new ProjectTask { Id = 1, Name = "ProjectTask 1", Description = "Description 1"},
                new ProjectTask { Id = 2, Name = "ProjectTask 2", Description = "Description 2"},
                new ProjectTask { Id = 3, Name = "ProjectTask 3", Description = "Description 3"}
            };

            _singleProjectTask = new ProjectTask { Id = 1, Name = "ProjectTask 1" };

            _singleProjectTaskDTO = new ProjectTaskDTO { Name = "ProjectTask 1" };            
        }

        [Fact]
        public async Task GetAllProjectTasks_ReturnsOkResultAndProjectTasksList()
        {
            // Arrange            
            // Set up the mock repository to return the 'projectTasks' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_projectTasksList);

            // Act
            var result = await _controller.GetAll(); // Call the GetAll method of the controller

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if the value of the OkObjectResult is of type IEnumerable<ProjectTaskDTOResponse>
            var model = Assert.IsAssignableFrom<IEnumerable<ProjectTaskDTO>>(okResult.Value);

            Assert.NotEmpty(model);// Check if the returned collection is not empty            
            Assert.Equal(200, okResult.StatusCode); // Verify if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
        }

        [Fact]
        public async Task GetProjectTaskById_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projectTask' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProjectTask);

            // Act
            var result = await _controller.GetById(1); // Call

            // Assert            
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify if status code is 200 (OK)
        }

        [Fact]
        public async Task CreateProjectTask_ReturnsOkResult()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService).Setup(service => service.WhiteSpaceValidation(_singleProjectTaskDTO)).Returns(true);

            // Act
            var result = await _controller.Create(_singleProjectTaskDTO); // Call

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify if status code is 200 (OK)
        }

        [Fact]
        public async Task DeleteProjectTask_ReturnsOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projectTask' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProjectTask);

            // Act
            var result = await _controller.Delete(1); // Call

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify if status code is 200 (OK)
        }

        [Fact]
        public async Task UpdateProjectTask_ReturnsOkResult()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService).Setup(service => service.WhiteSpaceValidation(_singleProjectTaskDTO)).Returns(true);

            // Set up the mock repository to return the 'projectTask' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProjectTask);

            var controller = new ProjectTaskController(_repository, _mapping, _validationService); // Create instance of controller with mocked dependencies

            // Act
            var result = await controller.Update(_singleProjectTaskDTO); // Call

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify if status code is 200 (OK)
        }
    }
}
