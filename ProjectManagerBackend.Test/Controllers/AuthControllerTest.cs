
namespace ProjectManagerBackend.Test.Controllers
{
    public class AuthControllerTest : IClassFixture<AuthControllerTest>
    {
        private readonly AuthController _controller;
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IValidationService> _validationServiceMock = new Mock<IValidationService>();
        private readonly Mock<IGenericRepository<UserDetail>> _repositoryMock = new Mock<IGenericRepository<UserDetail>>();
        private readonly Mock<IMappingService> _mappingServiceMock = new Mock<IMappingService>();
        private readonly Mock<IHashingService> _hashingServiceMock = new Mock<IHashingService>();
        private readonly Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();

        public AuthControllerTest()
        {
            _controller = new AuthController(
                _repositoryMock.Object,
                _mappingServiceMock.Object,
                _validationServiceMock.Object,
                _hashingServiceMock.Object,
                _userRepositoryMock.Object,
                _jwtServiceMock.Object
            // Initialize other mocked dependencies here
            );
        }

        [Fact]
        public async Task Create_ValidUser_ReturnsOk()
        {
            // Arrange
            var userDTO = new UserDetailDTO { FirstName = "validFirstName", LastName = "validLastName", Username = "validUsername", Password = "validPassword" };
            _userRepositoryMock.Setup(repo => repo.CheckUser(userDTO.Username)).ReturnsAsync(false);
            _validationServiceMock.Setup(vs => vs.WhiteSpaceValidation(userDTO)).Returns(true);

            // Act
            var result = await _controller.Create(userDTO);

            // Assert
            var okResult = Assert.IsType<ActionResult<UserDetailDTOResponse>>(result);
            var model = Assert.IsType<UserDetailDTOResponse>(okResult.Value);
            Assert.NotNull(model);
            // Add more assertions as needed
        }
    
        //private readonly IGenericRepository<UserDetail> _repository;
        //private readonly IMappingService _mapping;
        //private readonly IValidationService _validationService;
        //private readonly IHashingService _hashingService;
        //private readonly IUserRepository _userRepo;
        //private readonly IJwtService _jwtService;
        //private readonly AuthController _controller;


        //public AuthControllerTest() 
        //{
        //    _controller = new AuthController()
        //}
        //[Fact]
        //public async Task CreateUserTest()
        //{
        //    var mockRepo = new Mock<IGenericRepository<UserDetail>>();
        //    var mockMapping = new Mock<IMappingService>();
        //    var mockValidationService = new Mock<IValidationService>(); 
        //    var mockHashingService = new Mock<IHashingService>();
        //    var mockJwtService = new Mock<IJwtService>();
        //    var mockUserRepo = new Mock<IUserRepository>();

        //    var controller = new AuthController(mockRepo.Object, mockMapping.Object, mockValidationService.Object, mockHashingService.Object, mockUserRepo.Object, mockJwtService.Object);

        //    var userDetail = new UserDetailDTO
        //    {
        //        Username = "UseTHis",
        //        Password = "TestWord",
        //        FirstName = "TestFN",
        //        LastName = "TestLast"
        //    };

        //    var expectResponse = new UserDetailDTOResponse
        //    {
        //        Username = userDetail.Username,
        //        FirstName = userDetail.FirstName,
        //        LastName = userDetail.LastName,
        //    };

        //    var result = await controller.Create(userDetail);
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);

        //    //NOTE IF TESTING ASYNC SHIT MAKE TEST ASYNC

        //    Assert.Equal(200, okResult.StatusCode);            
        //}
    }
}
