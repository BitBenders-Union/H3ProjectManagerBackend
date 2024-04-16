using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskStatusController : GenericController<ProjectTaskStatus, ProjectTaskStatusDTO>
    {
        public ProjectTaskStatusController(
            IGenericRepository<ProjectTaskStatus> repository,
            IMappingService<ProjectTaskStatusDTO, ProjectTaskStatus> mapping
            ) : base(repository, mapping)
        {
        }
    }
}
