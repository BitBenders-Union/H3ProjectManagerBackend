using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;


namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : GenericController<Project, ProjectDTO>
    {

        // the constructor needs to be here, since we need to tell the generic controller what type of entity we are working with
        public ProjectController(
            IGenericRepository<Project> repository,
            IMappingService<ProjectDTO, Project> mapping
            ) : base(repository, mapping)
        {
        }

    }
}
