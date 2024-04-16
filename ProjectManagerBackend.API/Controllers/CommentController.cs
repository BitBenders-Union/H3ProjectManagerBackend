namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : GenericController<Comment>
    {
        public CommentController(IGenericRepository<Comment> repository) : base(repository)
        {
        }
    }
}
