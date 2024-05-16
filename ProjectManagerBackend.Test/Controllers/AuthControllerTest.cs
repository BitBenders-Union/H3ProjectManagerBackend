
namespace ProjectManagerBackend.Test.Controllers
{
    public class AuthControllerTest : IClassFixture<AuthControllerTest>
    {
        private readonly AuthController _controller;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IValidationService _validationServiceMock;
        private readonly IGenericRepository<UserDetail> _genRepositoryMock;
        private readonly IMappingService _mappingServiceMock;
        private readonly IHashingService _hashingServiceMock;
        private readonly IJwtService _jwtServiceMock;
        private readonly IUserRepository _usersRepositoryMock;
        public AuthControllerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>().Object;
            _validationServiceMock = new Mock<IValidationService>().Object;
            _genRepositoryMock = new Mock<IGenericRepository<UserDetail>>().Object;
            _mappingServiceMock = new Mock<IMappingService>().Object;
            _hashingServiceMock = new Mock<IHashingService>().Object;
            _jwtServiceMock = new Mock<IJwtService>().Object;
            _userRepositoryMock = new Mock<IUserRepository>().Object;


            _controller = new AuthController(
                _genRepositoryMock,
                _mappingServiceMock,
                _validationServiceMock,
                _hashingServiceMock,
                _userRepositoryMock,
                _jwtServiceMock,
                _usersRepositoryMock,
            );
        }

        [Fact]
        public async Task CreateUserTest_Return200()
        {

            var userDetail = new UserDetailDTO
            {
                Username = "UseTHis",
                Password = "TestWord",
                FirstName = "TestFN",
                LastName = "TestLast"
            };

            Mock.Get(_validationServiceMock).Setup(service => service.WhiteSpaceValidation(It.IsAny<UserDetailDTO>())).Returns(true);

            var result = await _controller.Create(userDetail);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            //NOTE IF TESTING ASYNC SHIT MAKE TEST ASYNC
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_ReturnOk()
        {
            Mock.Get(_repositoryMock).Setups(repo => repo.Update())
        }
    }

    }
}
