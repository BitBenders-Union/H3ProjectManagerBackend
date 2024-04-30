

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : GenericController<Client, ClientDTO, ClientDTO>
    {
        private readonly IMappingService _mapping;
        public ClientController(
            IGenericRepository<Client> repository, 
            IMappingService mapping,
            IValidationService validationService
            ): base(repository, mapping, validationService)
        {
            _mapping = mapping;
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClientDTO client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest("Client cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }
                                
                return Ok(await _repository.UpdateAsync(_mapping.Map<ClientDTO, Client>(client)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
