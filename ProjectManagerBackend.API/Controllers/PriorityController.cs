using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : GenericController<Priority>
    {
        public PriorityController(IGenericRepository<Priority> repository) : base(repository)
        {
        }
    }
}
