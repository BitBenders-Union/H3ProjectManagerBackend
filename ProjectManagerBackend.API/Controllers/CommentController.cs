
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : GenericController<Comment, CommentDTO, CommentDTO>
    {

        public CommentController(
            IGenericRepository<Comment> repository,
            IMappingService mapping,
            IValidationService validationService
            ) : base(repository, mapping, validationService)
        {

        }

        [HttpPut]
        public async Task<IActionResult> Update(CommentDTO comment)
        {
            try
            {
                if (comment == null)
                {
                    return BadRequest("Comment cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }
                                
                return Ok(await _repository.UpdateAsync(_mapping.Map<CommentDTO, Comment>(comment)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
