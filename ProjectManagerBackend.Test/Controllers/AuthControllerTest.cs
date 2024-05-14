
namespace ProjectManagerBackend.Test.Controllers
{
    public class AuthControllerTest : IClassFixture<AuthControllerTest>
    {
        //[Fact]
        public async Task CreateUserTest()
        {
            var mockRepo = new Mock<IGenericRepository<UserDetail>>();
            var mockMapping = new Mock<IMappingService>();
            var mockValidationService = new Mock<IValidationService>(); 
            var mockHashingService = new Mock<IHashingService>();
            var mockJwtService = new Mock<IJwtService>();
            var mockUserRepo = new Mock<IUserRepository>();

            var controller = new AuthController(mockRepo.Object, mockMapping.Object, mockValidationService.Object, mockHashingService.Object, mockUserRepo.Object, mockJwtService.Object);

            var userDetail = new UserDetailDTO
            {
                Username = "UseTHis",
                Password = "TestWord",
                FirstName = "Test First",
                LastName = "Test Last"
            };

            var expectResponse = new UserDetailDTOResponse
            {
                Username = userDetail.Username,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
            };

            var result = await controller.Create(userDetail);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            //NOTE IF TESTING ASYNC SHIT MAKE TEST ASYNC

            Assert.Equal(200, okResult.StatusCode);            
        }
    }
}
