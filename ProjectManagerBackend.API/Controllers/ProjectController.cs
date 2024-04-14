using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
=======
>>>>>>> Stashed changes

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
<<<<<<< Updated upstream
        private readonly ICrudInterface<Project> _repository;

        public ProjectController(ICrudInterface<Project> repository)
        {
            _repository = repository;
=======

        public ProjectController()
        {
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("GetProjects");
>>>>>>> Stashed changes
        }
    }
}
