

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : GenericController<Client, ClientDTO>
    {
        public ClientController(IGenericRepository<Client> repository, 
                                IMappingService<ClientDTO, Client> mapping
                                ) : base(repository, mapping)
        {
        }
    }
}
