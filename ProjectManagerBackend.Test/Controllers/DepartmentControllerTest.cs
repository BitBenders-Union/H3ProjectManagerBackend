namespace ProjectManagerBackend.Test.Controllers
{
    public class DepartmentControllerTest
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly List<Department> _departmentsList;
        private readonly Department _singleDepartment;

        public DepartmentControllerTest()
        {
            // Setup mocks
            _repository = new Mock<IGenericRepository<Department>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            _departmentsList = new List<Department>
            {
                new Department { Id = 1, Name = "Department 1" },
                new Department { Id = 2, Name = "Department 2" },
                new Department { Id = 3, Name = "Department 3" }
            };

            _singleDepartment = new Department { Id = 1, Name = "Department 1" };
        }

        [Fact]
        public async Task GetAllDepartments_ReturnOKResult()
        {
            // Arrange            
            // Set up the mock repository to return the 'departments' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_departmentsList);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Department, DepartmentDTO, DepartmentDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetAll();

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if the value of the OkObjectResult is of type IEnumerable<DepartmentDTOResponse>
            var model = Assert.IsAssignableFrom<IEnumerable<DepartmentDTO>>(okResult.Value);

            // Check if the returned collection is not empty
            Assert.NotEmpty(model);

            // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetDepartmentById_ReturnOkResult()
        {
            // Arrange         
            // Set up the mock repository to return the 'department' object when GetByIdAsync is called with 1 as parameter
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleDepartment); 
                        
            var controller = new GenericController<Department, DepartmentDTO, DepartmentDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetById(1); // Call GetById method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify status code
            
        }
    }
}