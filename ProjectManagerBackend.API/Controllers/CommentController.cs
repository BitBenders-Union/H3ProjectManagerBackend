
namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : GenericController<Comment, CommentDTO>
    {
        public CommentController(
            IGenericRepository<Comment> repository,
            IMappingService<CommentDTO, Comment> mapping
            ) : base(repository, mapping)
        {
        }
    }
}
