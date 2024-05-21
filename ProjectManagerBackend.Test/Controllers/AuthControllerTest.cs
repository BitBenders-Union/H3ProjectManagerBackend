
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
                _jwtServiceMock
                
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
            var userDTO = new UserDetailDTO
            {
                Id = 1,
                Username = "UseTHis",
                Password = "TestWord",
                FirstName = "TestFN",
                LastName = "TestLast"
            };

            Mock.Get(_userRepositoryMock).Setup(repo => repo.CheckUser(It.IsAny<string>())).ReturnsAsync(false);

            Mock.Get(_mappingServiceMock).Setup(mapping => mapping.AddUser(It.IsAny<UserDetailDTO>())).Returns(new UserDetail());

            Mock.Get(_userRepositoryMock).Setup(repo => repo.UpdateUser (It.IsAny<UserDetail>())).ReturnsAsync(true);


            var result = await _controller.Update(userDTO);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);

        }

    }
}
