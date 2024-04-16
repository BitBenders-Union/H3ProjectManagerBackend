using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using ProjectManagerBackend.API.Controllers;
using ProjectManagerBackend.Repo;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using ProjectManagerBackend.Repo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.Controllers
{
    public class AuthControllerTest : IClassFixture<AuthControllerTest>
    {
        [Fact]
        public void CreateUserTest()
        {
            var mockRepo = new Mock<IGenericRepository<UserDetail>>();
            var mockMapping = new Mock<IMappingService>();
            var mockUserRepo = new Mock<IUserRepository>();

            var controller = new AuthController(mockRepo.Object, mockMapping.Object, mockUserRepo.Object);

            var userDetail = new UserDetailDTO
            {
                Username = "UseTHis",
                Password = "TestWord",
                FirstName = "Test First",
                LastName = "Test Last"
            };

            var result = controller.Create(userDetail);
            var okResult = Assert.IsType<OkObjectResult>((OkObjectResult)result.Result);
            //var stats = (IStatusCodeActionResult)result;
            //var okResult = result as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
