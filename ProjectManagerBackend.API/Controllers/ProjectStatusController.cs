namespace ProjectManagerBackend.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatusController : GenericController<ProjectStatus, ProjectStatusDTO, ProjectStatusDTO>
    {
        private readonly IMappingService _mappingService;

        public ProjectStatusController(
            IGenericRepository<ProjectStatus> repository,
            IMappingService mapping) : base(repository, mapping)
        {
            _mappingService = mapping;
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectStatusDTO projectStatusDTO)
        {
            try
            {
                if (projectStatusDTO == null)
                {
                    return BadRequest("Project Status cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                return Ok(await _repository.UpdateAsync(_mapping.Map< ProjectStatusDTO, ProjectStatus>(projectStatusDTO)));


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
