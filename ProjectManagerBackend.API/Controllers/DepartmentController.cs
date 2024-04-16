using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department, DepartmentDTO>
    {
        public DepartmentController(
            IGenericRepository<Department> repository,
            IMappingService<DepartmentDTO, Department> mapping
            ) : base(repository, mapping)
        {
        }
    }
}
