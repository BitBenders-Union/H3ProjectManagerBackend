
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskCategoryController : GenericController<ProjectTaskCategory, ProjectTaskCategoryDTO, ProjectTaskCategoryDTO>
    {
        private readonly IMappingService _mappingService;
        public ProjectTaskCategoryController(
            IGenericRepository<ProjectTaskCategory> repository,
            IMappingService mapping
            ) : base(repository, mapping)
        {
            _mappingService = mapping;
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectTaskCategoryDTO projectTaskCategory)
        {
            try
            {
                if (projectTaskCategory == null)
                {
                    return BadRequest("Project Task Category cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                return Ok(await _repository.UpdateAsync(_mapping
                    .Map<ProjectTaskCategoryDTO, ProjectTaskCategory>(projectTaskCategory)));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
