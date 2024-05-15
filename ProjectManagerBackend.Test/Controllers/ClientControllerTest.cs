
namespace ProjectManagerBackend.Test.Controllers
{
    public class ClientControllerTest
    {
        private readonly IGenericRepository<Client> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<Client, ClientDTO, ClientDTO> _controller;

        private readonly List<Client> _clientsList;
        private readonly Client _singleClient;
        private readonly ClientDTO _singleClientDTO;

        public ClientControllerTest()
        {
            // Mocking the dependencies
            _repository = new Mock<IGenericRepository<Client>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<Client, ClientDTO, ClientDTO>(_repository, _mapping, _validationService);

            // Set up the data
            _clientsList = new List<Client>
            {
                new Client { Id = 1, Name = "Client 1" },
                new Client { Id = 2, Name = "Client 2" },
                new Client { Id = 3, Name = "Client 3" }
            };

            _singleClient = new Client { Id = 1, Name = "Client 1" };

            _singleClientDTO = new ClientDTO { Name = "Client 1" };
        }

        [Fact]
        public async Task GetAllDepartments_ReturnsOkResultAndDepartmentsList()
        {
            // Arrange
            // Set up the mock repository to use "GetAllAsync" method to return the 'clients' list
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_clientsList);
            
            // Act
            var result = await _controller.GetAll(); // Call the GetAll method

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var model = Assert.IsAssignableFrom<IEnumerable<ClientDTO>>(okResult.Value); // Check if the value of the OkObjectResult is of type IEnumerable<ClientDTO>

            Assert.NotEmpty(model); // Check if the returned collection is not empty            
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task GetClientById_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to use "GetByIdAsync" method to return the 'client' object
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleClient);

            // Act
            var result = await _controller.GetById(1); // Call GetById method with 1 as parameter

            // Assert

            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify if the result is of type OkObjectResult            
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task CreateClient_ReturnOkResultAndClient()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService)
                .Setup(service => service
                .WhiteSpaceValidation(It.IsAny<ClientDTO>()))
                .Returns(true);

            // Act
            var result = await _controller.Create(_singleClientDTO); // Call Create method with _singleClientDTO as parameter

            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task DeleteClient_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to use "DeleteAsync" method to return true
            Mock.Get(_repository).Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1); // Call Delete method with 1 as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }

        [Fact]
        public async Task UpdateClient_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to use "UpdateAsync" method to return true
            Mock.Get(_repository).Setup(repo => repo.UpdateAsync(_singleClient)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new ClientController(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Update(_singleClientDTO); // Call Update method with _singleClientDTO as parameter

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify if the result is of type OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Check if status code is 200 (OK)
        }   
    }
}
