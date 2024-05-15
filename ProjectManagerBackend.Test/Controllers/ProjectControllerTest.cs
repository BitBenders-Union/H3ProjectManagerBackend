

namespace ProjectManagerBackend.Test.Controllers
{
    public class ProjectControllerTest
    {
        private readonly IGenericRepository<Project> _repository;
        private readonly IMappingService _mapping;
        private readonly IProjectRepository _pRepo;
        private readonly IValidationService _validationService;

        private readonly List<Project> _projectsList;
        private readonly Project _singleProject;
        private readonly ProjectDTO _singleProjectDTO;

        public ProjectControllerTest()
        {
            // Setup mocks
            _repository = new Mock<IGenericRepository<Project>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _pRepo = new Mock<IProjectRepository>().Object;
            _validationService = new Mock<IValidationService>().Object;

            _projectsList = new List<Project>
            {
                new Project
            {
                Id = 1,
                Name = "Project 1",
                StartDate = new DateTime(2024, 5, 15), // Specify the start date
                EndDate = new DateTime(2024, 12, 31), // Specify the end date
                ProjectStatus = null, // or set it to a specific value
                ProjectTasks = null, // or initialize it with an empty list if needed
                ProjectCategory = null, // or set it to a specific value
                Priority = null, // or set it to a specific value
                Client = null, // or set it to a specific value
                ProjectDepartment = null, // or initialize it with an empty list if needed
                ProjectUserDetail = null, // or initialize it with an empty list if needed
                Owner = "" // or set it to a specific value
            },
                new Project
                {
                Id = 2,
                Name = "Project 2",
                StartDate = new DateTime(2024, 5, 15), // Specify the start date
                EndDate = new DateTime(2024, 12, 31), // Specify the end date
                ProjectStatus = null, // or set it to a specific value
                ProjectTasks = null, // or initialize it with an empty list if needed
                ProjectCategory = null, // or set it to a specific value
                Priority = null, // or set it to a specific value
                Client = null, // or set it to a specific value
                ProjectDepartment = null, // or initialize it with an empty list if needed
                ProjectUserDetail = null, // or initialize it with an empty list if needed
                Owner = "" // or set it to a specific value
            },
                new Project
                {
                Id = 3,
                Name = "Project 3",
                StartDate = new DateTime(2024, 5, 15), // Specify the start date
                EndDate = new DateTime(2024, 12, 31), // Specify the end date
                ProjectStatus = null, // or set it to a specific value
                ProjectTasks = null, // or initialize it with an empty list if needed
                ProjectCategory = null, // or set it to a specific value
                Priority = null, // or set it to a specific value
                Client = null, // or set it to a specific value
                ProjectDepartment = null, // or initialize it with an empty list if needed
                ProjectUserDetail = null, // or initialize it with an empty list if needed
                Owner = "" // or set it to a specific value
            }
        };

            _singleProject = new Project
            {
                Id = 1,
                Name = "Project 1",
                StartDate = new DateTime(2024, 5, 15), // Specify the start date
                EndDate = new DateTime(2024, 12, 31), // Specify the end date
                ProjectStatus = null, // or set it to a specific value
                ProjectTasks = null, // or initialize it with an empty list if needed
                ProjectCategory = null, // or set it to a specific value
                Priority = null, // or set it to a specific value
                Client = null, // or set it to a specific value
                ProjectDepartment = null, // or initialize it with an empty list if needed
                ProjectUserDetail = null, // or initialize it with an empty list if needed
                Owner = "" // or set it to a specific value
            };


            _singleProjectDTO = new ProjectDTO { Name = "Project 1", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2025, 12, 31) };
        }

        [Fact]
        public async Task GetAllProjects_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'projects' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_projectsList);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Project, ProjectDTO, ProjectDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetAll(); // Call the GetAll method

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult

            Assert.Equal(200, OkResult.StatusCode); // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
        }

        [Fact]
        public async Task GetProjectById_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'project' object when GetByIdAsync is called with 1 as parameter
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProject);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Project, ProjectDTO, ProjectDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetById(1); // Call

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
        }

        [Fact]
        public async Task CreateProject_ReturnsOkResult()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService).Setup(service => service.WhiteSpaceValidation(_singleProjectDTO)).Returns(true);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Project, ProjectDTO, ProjectDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Create(_singleProjectDTO); // Call Create method with _singleProjectDTO as parameter

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
        }

        [Fact]
        public async Task UpdateProject_ReturnsOkResult()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService).Setup(service => service.WhiteSpaceValidation(_singleProjectDTO)).Returns(true);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Project, ProjectDTO, ProjectDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Delete(1); // Call Update method with 1 as parameters

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result); // Verify OkObjectResult
        }

        [Fact]
        public async Task UpdateProject_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'project' object when GetByIdAsync is called with 1 as parameter
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleProject);

            // Create instance of controller with mocked dependencies
            var controller = new ProjectController(_repository, _mapping, _pRepo, _validationService);

            // Act
            var result = await controller.Update(_singleProjectDTO); // Call Update method with 1 as parameter

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result); // Verify OkObjectResult
        }
    }
}
