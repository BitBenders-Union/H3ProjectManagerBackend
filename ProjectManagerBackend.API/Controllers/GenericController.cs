using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBackend.Repo.Interfaces;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TEntityDTO, TEntityDTOResponse> : ControllerBase 
        where TEntity : class
        where TEntityDTO : class
        where TEntityDTOResponse : class
    {
        public readonly IGenericRepository<TEntity> _repository;
        public readonly IMappingService _mapping;

        public GenericController(
            IGenericRepository<TEntity> repository,
            IMappingService mapping
            )
        {
            _repository = repository;
            _mapping = mapping;
        }

        [HttpPost]
        public async virtual Task<ActionResult<TEntityDTOResponse>> Create(TEntityDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity Can not be null");

                if (!ModelState.IsValid)
                    return BadRequest("Modelstate is Invalid");

                var result = await _repository.CreateAsync(_mapping.Map<TEntityDTO, TEntity>(entity));

                return Ok(_mapping.Map<TEntity, TEntityDTOResponse>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async virtual Task<ActionResult<IEnumerable<TEntityDTOResponse>>> GetAll()
        {
            try
            {

                var items = await _repository.GetAllAsync();
                if (items == null)
                {
                    return NotFound();
                }

                var result = new List<TEntityDTOResponse>();
                
                foreach ( var item in items )
                {
                    result.Add(_mapping.Map<TEntity, TEntityDTOResponse>(item));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<TEntityDTOResponse>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(_mapping.Map<TEntity, TEntityDTOResponse>(item));
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
