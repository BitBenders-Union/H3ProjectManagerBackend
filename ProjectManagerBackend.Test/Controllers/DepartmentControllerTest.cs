namespace ProjectManagerBackend.Test.Controllers
{
    public class DepartmentControllerTest
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        public DepartmentControllerTest()
        {
            // Setup mocks
            _repository = new Mock<IGenericRepository<Department>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;
        }

        [Fact]
        public async Task GetAllDepartments_ReturnOKResult()
        {
            // Arrange
            // Mock repository to return some data
            var departments = new List<Department> { new Department { /* initialize department properties */ } };
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(departments);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Department, DepartmentDTO, DepartmentDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<DepartmentDTO>>(okResult.Value);
            Assert.NotEmpty(model); // Check if the returned collection is not empty
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }
    }
}
