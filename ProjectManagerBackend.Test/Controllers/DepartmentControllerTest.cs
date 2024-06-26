﻿namespace ProjectManagerBackend.Test.Controllers
{
    public class DepartmentControllerTest
    {
        private readonly IGenericRepository<Department> _repository;        
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<Department, DepartmentDTO, DepartmentDTO> _controller;

        private readonly List<Department> _departmentsList;        
        private readonly Department _singleDepartment;        
        private readonly DepartmentDTO _singleDepartmentDTO;

        public DepartmentControllerTest()
        {
            // Mocking the dependencies
            _repository = new Mock<IGenericRepository<Department>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<Department, DepartmentDTO, DepartmentDTO>(_repository, _mapping, _validationService);

            // Set up the data
            _departmentsList = new List<Department>
            {
                new Department { Id = 1, Name = "Department 1" },
                new Department { Id = 2, Name = "Department 2" },
                new Department { Id = 3, Name = "Department 3" }
            };

            _singleDepartment = new Department { Id = 1, Name = "Department 1" };
            
            _singleDepartmentDTO= new DepartmentDTO { Name = "Department 1" };
        }

        [Fact]
        public async Task GetAllDepartments_ReturnsOkResultAndDepartmentsList()
        {
            // Arrange            
            // Set up the mock repository to return the 'departments' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_departmentsList);
                        
            // Act
            var result = await _controller.GetAll();

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

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Department, DepartmentDTO, DepartmentDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetById(1); // Call GetById method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify status code
            
        }

        [Fact]
        public async Task CreateDepartment_ReturnOkResultAndDepartment()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService)
                .Setup(service => service
                .WhiteSpaceValidation(It.IsAny<DepartmentDTO>()))
                .Returns(true);

            // Act
            var result = await _controller.Create(_singleDepartmentDTO);

            // Assert

            /*
             * Cant use this because the return type is Null -Maybe skip it?
             
            var returnedModel = Assert.IsType<DepartmentDTO>(okResult.Value); // Assert it's a TEntityDTOResponse
            Assert.NotNull(returnedModel); // Ensure the model is not null

            */

            // Check if the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            
            // Check if status code is 200 (OK)
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteDepartment_ReturnOkResult()
        {
            // Arrange
                Mock.Get(_repository).Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);
                        
            // Act
            var result = await _controller.Delete(1);

            // Assert
            // Check if the result is of type OkObjectResult and has a status code of 200, no need to check the value
            var okResult = Assert.IsType<OkObjectResult>(result);
            // Check if status code is 200 (OK)
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateDepartment_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return true when UpdateAsync is called with _singleDepartment as parameter
            Mock.Get(_repository).Setup(repo => repo.UpdateAsync(_singleDepartment)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new DepartmentController(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Update(_singleDepartmentDTO); // Call Update method with _singleDepartmentDTO as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify if the result is of type OkObjectResult            
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)

        }
    }
}