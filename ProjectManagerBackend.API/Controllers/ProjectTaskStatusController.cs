using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskStatusController : GenericController<ProjectTaskStatus>
    {
        public ProjectTaskStatusController(IGenericRepository<ProjectTaskStatus> repository) : base(repository)
        {
        }
    }
}
