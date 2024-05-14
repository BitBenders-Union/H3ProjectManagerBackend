
namespace ProjectManagerBackend.Test.Repositories
{
    public class CommentRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;


        public CommentRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockComment").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Comments.Add(new Comment { Id = 1, Title = "Test Comment 1", Description = "Test Description 1" });
            _context.Comments.Add(new Comment { Id = 2, Title = "Test Comment 2", Description = "Test Description 2" });
            _context.Comments.Add(new Comment { Id = 3, Title = "Test Comment 3", Description = "Test Description 3" });   

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllComments_ReturnList()
        {
            // Arrange
            GenericRepository<Comment> repository = new(_context);
            
            // Act
            var commentList = repository.GetAllAsync();
            commentList.Wait();
            var list = commentList.Result.ToList();
            
            // Assert
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task GetCommentById_ReturnOneComment()
        {
            // Arrange
            GenericRepository<Comment> repository = new(_context);

            // Act
            Comment comment = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, comment.Id);
            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task AddComment_ReturnComment()
        {
            Comment comment = new Comment { Title = "Test Comment 4", Description = "Test Description 4" };
            // Arrange
            GenericRepository<Comment> repository = new(_context);
            // Act
            Comment returnedComment = await repository.CreateAsync(comment);
            // Assert
            Assert.Equal(returnedComment, comment);
        }

        [Fact]
        public async Task DeleteComment_ReturnTrue()
        {
            Comment comment = new Comment { Title = "Test Comment 1", Description = "Test Description 1" };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            // Arrange
            GenericRepository<Comment> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(comment.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateComment_ReturnComment()
        {
            // Arrange
            GenericRepository<Comment> repository = new(_context);
            Comment comment = await repository.GetByIdAsync(1);
            comment.Title = "Test Comment 1 Updated";
            comment.Description = "Test Description 1 Updated";

            // Act
            bool result = await repository.UpdateAsync(comment);

            // Assert
            Assert.True(result);
        }
    }
}
