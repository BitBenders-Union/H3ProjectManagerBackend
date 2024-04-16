using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBackend.Repo.Interfaces;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TEntityDTO> : ControllerBase 
        where TEntity : class
        where TEntityDTO : class
    {
        public readonly IGenericRepository<TEntity> _repository;
        public readonly IMappingService<TEntityDTO, TEntity> _mapping;

        public GenericController(
            IGenericRepository<TEntity> repository,
            IMappingService<TEntityDTO, TEntity> mapping
            )
        {
            _repository = repository;
            _mapping = mapping;
        }

        [HttpPost]
        public async virtual Task<ActionResult<TEntity>> Create(TEntityDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity Can not be null");

                if (!ModelState.IsValid)
                    return BadRequest("Modelstate is Invalid");


                return Ok(await _repository.CreateAsync(_mapping.Map(entity)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async virtual Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<TEntity>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpDelete]
        public async virtual Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest("Id can not be null");

                if (!ModelState.IsValid)
                    return BadRequest("Modelstate Invalid");

                return Ok(await _repository.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
