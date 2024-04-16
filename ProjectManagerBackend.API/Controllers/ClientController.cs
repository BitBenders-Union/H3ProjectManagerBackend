

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : GenericController<Client, ClientDTO, ClientDTO>
    {
        public ClientController(IGenericRepository<Client> repository, 
                                IMappingService mapping
                                ) : base(repository, mapping)
        {
        }
    }
}
