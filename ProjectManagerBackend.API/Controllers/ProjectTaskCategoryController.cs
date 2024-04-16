using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskCategoryController : GenericController<ProjectTaskCategory, ProjectTaskCategoryDTO, ProjectTaskCategoryDTO>
    {
        public ProjectTaskCategoryController(
            IGenericRepository<ProjectTaskCategory> repository,
            IMappingService mapping
            ) : base(repository, mapping)
        {
        }
    }
}
