
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : GenericController<Client>
    {
        public ClientController(IGenericRepository<Client> repository) : base(repository)
        {
        }
    }
}
