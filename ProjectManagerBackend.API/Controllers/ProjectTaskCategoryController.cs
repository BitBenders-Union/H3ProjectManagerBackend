using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskCategoryController : GenericController<ProjectTaskCategory>
    {
        public ProjectTaskCategoryController(IGenericRepository<ProjectTaskCategory> repository) : base(repository)
        {
        }
    }
}
