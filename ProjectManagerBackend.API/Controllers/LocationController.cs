using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : GenericController<Location, LocationDTO, LocationDTO>
    {
        public LocationController(
            IGenericRepository<Location> repository,
            IMappingService mapping            
            ) : base(repository, mapping)
        {
        }
    }
}
