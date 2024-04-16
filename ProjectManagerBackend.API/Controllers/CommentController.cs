
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : GenericController<Comment, CommentDTO, CommentDTO>
    {
        public CommentController(
            IGenericRepository<Comment> repository,
            IMappingService mapping
            ) : base(repository, mapping)
        {
        }
    }
}
