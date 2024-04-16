using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericController<Role, RoleDTO, RoleDTO>
    {
        public RoleController(
            IGenericRepository<Role> repository,
            IMappingService mapping
            ) : base(repository, mapping)
        {
        }
    }
}
