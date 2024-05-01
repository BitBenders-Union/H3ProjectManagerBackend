
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : GenericController<Project, ProjectDTO, ProjectDTO>
    {
        private readonly IProjectRepository _projectRepository;

        // the constructor needs to be here, since we need to tell the generic controller what type of entity we are working with
        public ProjectController(
            IGenericRepository<Project> repository,
            IMappingService mapping,
            IProjectRepository pRepo,
            IValidationService validationService
            ) : base(repository, mapping, validationService)
        {
            _projectRepository = pRepo;
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectDTO projectDTO)
        {
            try
            {
                if (projectDTO == null)
                {
                    return BadRequest("Project cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                return Ok(await _repository.UpdateAsync(_mapping.Map<ProjectDTO, Project>(projectDTO)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAll/{userid}")]
        public async Task<ActionResult<ProjectDashboardDTO>> GetForUser(int userid)
        {
            try
            {
                var result = await _projectRepository.GetAllProjectDashboards(userid);
                List<ProjectDashboardDTO> pdDTO = new();

                    foreach ( var project in result.Projects)
                    {
                        ProjectDashboardDTO entity = new()
                        {
                            Id = project.Id,
                            Name = project.Name,
                            Category = project.ProjectCategory.Name,
                            Owner = project.Owner

                        }; 
                        pdDTO.Add(entity);
                    }
                return Ok(pdDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
