namespace ProjectManagerBackend.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatusController : GenericController<ProjectStatus, ProjectStatusDTO, ProjectStatusDTO>
    {

        public ProjectStatusController(
            IGenericRepository<ProjectStatus> repository,
            IMappingService mapping,
            IValidationService validationService) : base(repository, mapping, validationService)
        {
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
                if(!_validationService.WhiteSpaceValidation(projectStatusDTO))
                {
                    return BadRequest("Project Status cannot be empty");
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
