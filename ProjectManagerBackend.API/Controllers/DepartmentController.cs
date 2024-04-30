using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department, DepartmentDTO, DepartmentDTO>
    {

        public DepartmentController(
            IGenericRepository<Department> repository,
            IMappingService mapping,
            IValidationService validationService
            ) : base(repository, mapping, validationService)
        {

        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentDTO department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Department cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }
                return Ok(await _repository.UpdateAsync(_mapping.Map<DepartmentDTO, Department>(department)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
