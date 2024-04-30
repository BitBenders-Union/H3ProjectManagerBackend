
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskStatusController : GenericController<ProjectTaskStatus, ProjectTaskStatusDTO, ProjectTaskStatusDTO>
    {
        public ProjectTaskStatusController(
            IGenericRepository<ProjectTaskStatus> repository,
            IMappingService mapping,
            IValidationService validationService
            ) : base(repository, mapping, validationService)
        {
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectTaskStatusDTO projectTaskStatusDTO)
        {
            try
            {
                if (projectTaskStatusDTO == null)
                {
                    return BadRequest("Role cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                return Ok(await _repository.UpdateAsync(_mapping.Map<ProjectTaskStatusDTO, ProjectTaskStatus>(projectTaskStatusDTO)));
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
