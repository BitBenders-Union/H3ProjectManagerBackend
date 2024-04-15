using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBackend.Repo.Interfaces;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity> : ControllerBase where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericController(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
    }
}
