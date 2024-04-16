using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : GenericController<ProjectTask, ProjectTaskDTO>
    {
        public ProjectTaskController(
            IGenericRepository<ProjectTask> repository,
            IMappingService<ProjectTaskDTO, ProjectTask> mapping
            ) : base(repository, mapping)
        {
        }
    }
}
