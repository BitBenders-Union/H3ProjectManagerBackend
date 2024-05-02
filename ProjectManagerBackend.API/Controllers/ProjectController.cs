
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


        [HttpGet("GetForUser/{userid}")]
        public async Task<ActionResult<ProjectDashboardDTO>> GetForUser(int userid)
        {
            try
            {
                var result = await _projectRepository.GetAllProjectDashboards(userid);

                // map result to projectDashboardDTO

                List<ProjectDashboardDTO> dtoResponses = new();
                foreach (var response in result)
                {
                    dtoResponses.Add(new ProjectDashboardDTO
                    {
                        Id = response.Id,
                        Name = response.Name,
                        Category = response.ProjectCategory.Name,
                        Owner = response.Owner
                    });
                }

                return Ok(dtoResponses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
