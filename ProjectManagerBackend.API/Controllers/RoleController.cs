using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericController<Role>
    {
        public RoleController(IGenericRepository<Role> repository) : base(repository)
        {
        }
    }
}
