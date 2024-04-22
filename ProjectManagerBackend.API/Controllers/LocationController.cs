
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : GenericController<Location, LocationDTO, LocationDTO>
    {
        private readonly IMappingService _mappingService;
        public LocationController(
            IGenericRepository<Location> repository,
            IMappingService mapping            
            ) : base(repository, mapping)
        {
            _mappingService = mapping;
        }

        [HttpPut]
        public async Task<IActionResult> Update(LocationDTO location)
        {
            try
            {
                if (location == null)
                {
                    return BadRequest("Location cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }
                return Ok(await _repository.UpdateAsync(_mapping.Map<LocationDTO, Location>(location)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
