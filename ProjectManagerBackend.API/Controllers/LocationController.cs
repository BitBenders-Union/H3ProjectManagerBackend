using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : GenericController<Location, LocationDTO>
    {
        public LocationController(
            IGenericRepository<Location> repository,
            IMappingService<LocationDTO, Location> mapping            
            ) : base(repository, mapping)
        {
        }
    }
}
