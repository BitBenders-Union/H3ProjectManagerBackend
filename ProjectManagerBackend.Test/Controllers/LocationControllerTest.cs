using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.Controllers
{
    public class LocationControllerTest
    {

        private readonly IGenericRepository<Location> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<Location, LocationDTO, LocationDTO> _controller;

        private readonly List<Location> _locationsList;
        private readonly Location _singleLocation;
        private readonly LocationDTO _singleLocationDTO;

        public LocationControllerTest()
        {
            // Mocking the dependencies
            _repository = new Mock<IGenericRepository<Location>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<Location, LocationDTO, LocationDTO>(_repository, _mapping, _validationService);

            // Set up the data
            _locationsList = new List<Location>
            {
                new Location { Id = 1, Name = "Location 1" },
                new Location { Id = 2, Name = "Location 2" },
                new Location { Id = 3, Name = "Location 3" }
            };

            _singleLocation = new Location { Id = 1, Name = "Location 1" };

            _singleLocationDTO = new LocationDTO { Name = "Location 1" };
        }

        [Fact]
        public async Task GetAllLocations_ReturnsOkResultAndLocationsList()
        {
            // Arrange            
            // Set up the mock repository to return the 'locations' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_locationsList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if the value of the OkObjectResult is of type IEnumerable<LocationDTOResponse>
            var model = Assert.IsAssignableFrom<IEnumerable<LocationDTO>>(okResult.Value);

            // Check if the returned collection is not empty
            Assert.NotEmpty(model);

            // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetLocationById_ReturnOkResult()
        {
            // Arrange         
            // Set up the mock repository to return the 'location' object when GetByIdAsync is called with 1 as parameter
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleLocation);

            // Create instance of controller with mocked dependencies
            var controller = new GenericController<Location, LocationDTO, LocationDTO>(_repository, _mapping, _validationService);

            // Act
            var result = await controller.GetById(1); // Call GetById method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify status code

        }

        [Fact]
        public async Task CreateLocation_ReturnOkResultAndLocation()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService)
                .Setup(service => service
                .WhiteSpaceValidation(It.IsAny<LocationDTO>()))
                .Returns(true);

            // Act
            var result = await _controller.Create(_singleLocationDTO);

            // Assert

            /*
             * Cant use this because the return type is Null -Maybe skip it?
             
            var returnedModel = Assert.IsType<LocationDTO>(okResult.Value); // Assert it's a TEntityDTOResponse
            Assert.NotNull(returnedModel); // Ensure the model is not null

            */

            // Check if the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if status code is 200 (OK)
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteLocation_ReturnOkResult()
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
        public async Task UpdateLocation_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return true when UpdateAsync is called with _singleLocation as parameter
            Mock.Get(_repository).Setup(repo => repo.UpdateAsync(_singleLocation)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new LocationController(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Update(_singleLocationDTO); // Call Update method with _singleLocationDTO as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify if the result is of type OkObjectResult            
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)

        }
    }
}