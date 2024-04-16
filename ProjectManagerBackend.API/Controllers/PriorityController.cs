using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : GenericController<Priority, PriorityDTO, PriorityDTO>
    {
        public PriorityController(IGenericRepository<Priority> repository,
            IMappingService mapping) : base(repository, mapping)
        {
        }
    }
}
