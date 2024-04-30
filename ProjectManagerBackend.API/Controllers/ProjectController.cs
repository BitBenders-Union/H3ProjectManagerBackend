
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


        [HttpGet("getAll/{userid}")]
        public async Task<ActionResult<ProjectDashboardDTO>> GetAllSpecial(int userid)
        {

            var category = new ProjectCategoryDTO()
            {
                Id = 1,
                Name = "test"
            };

            var result2 = new ProjectDashboardDTO()
            {
                Id = 1,
                Name = "Project - Title",
                Category = category.Name,
                Owner = "test - Owner"
            };

            List<ProjectDashboardDTO> pjl = new List<ProjectDashboardDTO>();
            pjl.Add(result2);
            return Ok(pjl);
            try
            {
                var result = await _projectRepository.GetAllProjectDashboards(userid);
                List<ProjectDashboardDTO> pjDTO = new();

                    foreach ( var project in result.Projects)
                    {
                        ProjectDashboardDTO entity = new()
                        {
                            Id = project.Id,
                            Name = project.Name,
                            Category = project.ProjectCategory.Name,
                            Owner = await _projectRepository.GetOwnerName(project.OwnerId)

                        }; 
                        pjDTO.Add(entity);
                    }
                return Ok(pjDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
