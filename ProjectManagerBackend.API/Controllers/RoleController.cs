using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericController<Role, RoleDTO>
    {
        public RoleController(
            IGenericRepository<Role> repository,
            IMappingService<RoleDTO, Role> mapping
            ) : base(repository, mapping)
        {
        }
    }
}
