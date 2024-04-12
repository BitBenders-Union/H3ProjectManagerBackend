using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ICrudInterface<Project> _repository;

        public ProjectController(ICrudInterface<Project> repository)
        {
            _repository = repository;
        }
    }
}
