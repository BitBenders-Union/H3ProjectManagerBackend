using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskStatusController : GenericController<ProjectTaskStatus, ProjectTaskStatusDTO, ProjectTaskStatusDTO>
    {
        public ProjectTaskStatusController(
            IGenericRepository<ProjectTaskStatus> repository,
            IMappingService mapping
            ) : base(repository, mapping)
        {
        }
    }
}
