
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : GenericController<Priority, PriorityDTO, PriorityDTO>
    {

        public PriorityController(
            IGenericRepository<Priority> repository,
            IMappingService mapping,
            IValidationService validationService
            ) : base(repository, mapping, validationService)
        {

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

                if (!_validationService.WhiteSpaceValidation(priorityDTO))
                    return BadRequest("Cannot contain whitespace");

                return Ok(await _repository.UpdateAsync(_mapping.Map<PriorityDTO, Priority>(priorityDTO)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
