using Microsoft.AspNetCore.Authorization;
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
            IMappingService mapping,
            IValidationService validationService
            ) : base(repository, mapping , validationService)
        {
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleDTO role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest("Role cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }
                                
                return Ok(await _repository.UpdateAsync(_mapping.Map<RoleDTO, Role>(role)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
