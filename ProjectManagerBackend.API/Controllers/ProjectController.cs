
using ProjectManagerBackend.Repo.DTOs;

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

                return Ok(await _repository.UpdateAsync(_mapping.ProjectMapping(projectDTO)));

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

                if(userid == 0)
                {
                    return BadRequest("Userid cannot be 0");
                }

                var result = await _projectRepository.GetAllProjectDashboards(userid);

                if(result == null)
                {
                    return NotFound("No projects found for user");
                }

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

        [HttpGet("{id}")]
        public async override Task<ActionResult<ProjectDTO>> GetById(int id)
        {
            try
            {
                var result = await _projectRepository.GetProject(id);
                if (result == null)
                {
                    return NotFound();
                }    

                var mapping = _mapping.ProjectMapping(result);

                return Ok(mapping);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message); 
            }
        }


        [HttpPost]
        public override async Task<ActionResult<ProjectDTO>> Create(ProjectDTO dto)
        {


            try
            {
                // validate

                if (dto == null)
                {
                    return BadRequest("Project cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                if(!_validationService.WhiteSpaceValidation(dto))
                {
                    return BadRequest("Invalid Model, Must not contain empty whitespace!");
                }

                // map

                var model = await _mapping.ProjectCreateMapping(dto);

                // then create

                model = await _repository.CreateAsync(model);

                // create many to many with newly created entity

                _projectRepository.CreateManyToMany(model.Id, dto.Users.First().Id);

                
                // then return

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
