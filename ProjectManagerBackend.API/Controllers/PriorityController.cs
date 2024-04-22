
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : GenericController<Priority, PriorityDTO, PriorityDTO>
    {
        private readonly IMappingService _mapping;
        public PriorityController(IGenericRepository<Priority> repository,
            IMappingService mapping) : base(repository, mapping)
        {
            _mapping = mapping;
        }

        [HttpPut]
        public async Task<IActionResult> Update(PriorityDTO priorityDTO)
        {
            try
            {
                if (priorityDTO == null)
                {
                    return BadRequest("Priority cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                return Ok(await _repository.UpdateAsync(_mapping.Map<PriorityDTO, Priority>(priorityDTO)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
