﻿
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : GenericController<ProjectTask, ProjectTaskDTO, ProjectTaskDTO>
    {
        private readonly IMappingService _mappingService;
        public ProjectTaskController(
            IGenericRepository<ProjectTask> repository,
            IMappingService mapping
            ) : base(repository, mapping)
        {
            _mappingService = mapping;
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

                return Ok(await _repository.UpdateAsync(_mapping
                    .Map<ProjectTaskDTO, ProjectTask>(projectTaskDTO)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
