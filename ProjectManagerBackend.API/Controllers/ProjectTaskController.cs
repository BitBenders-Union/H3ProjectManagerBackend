
using ProjectManagerBackend.Repo.DTOs;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : GenericController<ProjectTask, ProjectTaskDTO, ProjectTaskDTO>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        public ProjectTaskController(
            IGenericRepository<ProjectTask> repository,
            IMappingService mapping,
            IValidationService validationService,
            IProjectTaskRepository projectTaskRepository
            ) : base(repository, mapping, validationService)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectTaskDTO projectTaskDTO)
        {
            try
            {
                if (projectTaskDTO == null)
                {
                    return BadRequest("Role cannot be null");
                }

                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                var mapped = await _mapping.ProjectTaskUpdateMapping(projectTaskDTO);

                var updateReturn = await _repository.UpdateAsync(mapped);

                return Ok(updateReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async override Task<ActionResult<ProjectTaskDTO>> Create(ProjectTaskDTO dto)
        {
            try
            {
                // validate

                if (dto == null)
                {
                    return BadRequest("Role cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                // map
                var mapped = await _mapping.ProjectTaskCreateMapping(dto);


                // create
                // create mapped object

                mapped = await _repository.CreateAsync(mapped);

                // create many to many with newly created entity
                // foreach dto.ProjectTaskUserDetail
                foreach (var user in dto.ProjectTaskUserDetail)
                {
                    _projectTaskRepository.CreateManyToMany(mapped.Id, user.Id);
                }

                // then return ok
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
