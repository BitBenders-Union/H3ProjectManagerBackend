
namespace ProjectManagerBackend.Test.Controllers
{
    public class RoleControllerTest    
    {
        private readonly IGenericRepository<Role> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<Role, RoleDTO, RoleDTO> _controller;

        private readonly List<Role> _roleList;
        private readonly Role _singleRole;
        private readonly RoleDTO _singleRoleDTO;

        public RoleControllerTest()
        {
            // Mocking the dependencies
            _repository = new Mock<IGenericRepository<Role>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<Role, RoleDTO, RoleDTO>(_repository, _mapping, _validationService);

            // Set up the data
            _roleList = new List<Role>
            {
                new Role { Id = 1, Name = "Role 1" },
                new Role { Id = 2, Name = "Role 2" },
                new Role { Id = 3, Name = "Role 3" }
            };

            _singleRole = new Role { Id = 1, Name = "Role 1" };

            _singleRoleDTO = new RoleDTO { Name = "Role 1" };
        }

        [Fact]
        public async Task GetAllRole_ReturnsOkResultAndRolesList()
        {
            // Arrange            
            // Set up the mock repository to return the 'Role's list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_roleList);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Role, RoleDTO, RoleDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetAll();

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if the value of the OkObjectResult is of type IEnumerable<RoleDTOResponse>
            var model = Assert.IsAssignableFrom<IEnumerable<RoleDTO>>(okResult.Value);

            // Check if the returned collection is not empty
            Assert.NotEmpty(model);

            // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRoleById_ReturnOkResult()
        {
            // Arrange         
            // Set up the mock repository to return the 'Role' object when GetByIdAsync is called with 1 as parameter
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleRole);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Role, RoleDTO, RoleDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetById(1); // Call GetById method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify status code

        }

        [Fact]
        public async Task CreateRole_ReturnOkResultAndRole()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService)
                .Setup(service => service
                .WhiteSpaceValidation(It.IsAny<RoleDTO>()))
                .Returns(true);

            // Controller instance
            var controller = new GenericController<Role, RoleDTO, RoleDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Create(_singleRoleDTO);

            // Assert

            /*
             * Cant use this because the return type is Null -Maybe skip it?

            var returnedModel = Assert.IsType<RoleDTO>(okResult.Value); // Assert it's a TEntityDTOResponse
            Assert.NotNull(returnedModel); // Ensure the model is not null

            */

            // Check if the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if status code is 200 (OK)
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteRole_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return true when DeleteAsync is called with 1 as parameter
            Mock.Get(_repository).Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Role, RoleDTO, RoleDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Delete(1);

            // Assert
            // Check if the result is of type OkObjectResult and has a status code of 200, no need to check the value
            var okResult = Assert.IsType<OkObjectResult>(result);
            // Check if status code is 200 (OK)
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateRole_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return true when UpdateAsync is called with _singleRole as parameter
            Mock.Get(_repository).Setup(repo => repo.UpdateAsync(_singleRole)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new RoleController(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Update(_singleRoleDTO); // Call Update method with _singleRoleDTO as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify if the result is of type OkObjectResult            
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)

        }
    }
}