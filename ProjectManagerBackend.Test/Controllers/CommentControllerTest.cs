
namespace ProjectManagerBackend.Test.Controllers
{
    public class CommentControllerTest
    {
        private readonly IGenericRepository<Comment> _repository;
        private readonly IMappingService _mapping;
        private readonly IValidationService _validationService;

        private readonly GenericController<Comment, CommentDTO, CommentDTO> _controller;

        private readonly List<Comment> _commentsList;
        private readonly Comment _singleComment;
        private readonly CommentDTO _singleCommentDTO;

        public CommentControllerTest()
        {
            // Mocking the dependencies
            _repository = new Mock<IGenericRepository<Comment>>().Object;
            _mapping = new Mock<IMappingService>().Object;
            _validationService = new Mock<IValidationService>().Object;

            // Create instance of controller with mocked dependencies
            _controller = new GenericController<Comment, CommentDTO, CommentDTO>(_repository, _mapping, _validationService);

            // Set up the data
            _commentsList = new List<Comment>
            {
               new Comment { Id = 1, Title = "Comment 1" , Description = "Description 1"},
               new Comment { Id = 2, Title = "Comment 2" , Description = "Description 2"},
               new Comment { Id = 3, Title = "Comment 3" , Description = "Description 3"}
            };

            _singleComment = new Comment { Id = 1, Title = "Comment 1", Description = "Description 1" };

            _singleCommentDTO = new CommentDTO { Title = "Comment 1", Description = "Description 1" };
        }

        [Fact]
        public async Task GetAllComments_ReturnsOkResultAndCommentsList()
        {
            // Arrange            
            // Set up the mock repository to return the 'comments' list when GetAllAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(_commentsList);
                       
            // Act
            var result = await _controller.GetAll(); // Call the GetAll method of the controller

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check if the value of the OkObjectResult is of type IEnumerable<CommentDTOResponse>
            var model = Assert.IsAssignableFrom<IEnumerable<CommentDTO>>(okResult.Value);
                        
            Assert.NotEmpty(model);// Check if the returned collection is not empty            
            Assert.Equal(200, okResult.StatusCode); // Verify if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
        }

        [Fact]
        public async Task GetCommentById_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'comment' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleComment);                        

            // Act
            var result = await _controller.GetById(1); // Call the GetById method of the controller

            // Assert
            // Check if the result is of type OkObjectResult, saves "instance" of OkObjectResult in okResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);            

            // Check if status code is 200 (OK), So okResult is an instance of OkObjectResult which has a StatusCode property
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateComment_ReturnOkResult()
        {
            // Arrange
            // Because the actual validation service (_validationService) might be failing even with a valid DTO object due to potential whitespace checks or other validation logic,**
            // we temporarily bypass the validation for this specific test. This allows us to focus on testing the controller's logic for successful creation.
            Mock.Get(_validationService).Setup(service => service.WhiteSpaceValidation(_singleCommentDTO)).Returns(true);

            // Act
            var result = await _controller.Create(_singleCommentDTO); // Call the Create method of the controller

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verify OkObjectResult            
            Assert.Equal(200, okResult.StatusCode); // Verify status code
        }

        [Fact]
        public async Task DeleteComment_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return the 'comment' when GetByIdAsync is called
            Mock.Get(_repository).Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_singleComment);

            // Act
            var result = await _controller.Delete(1); // Call the Delete method of the controller

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify status code
        }

        [Fact]
        public async Task UpdateComment_ReturnOkResult()
        {
            // Arrange
            // Set up the mock repository to return true when UpdateAsync is called with _singleComment as parameter
            Mock.Get(_repository).Setup(repo => repo.UpdateAsync(_singleComment)).ReturnsAsync(true);

            // Create instance of controller with mocked dependencies
            var controller = new CommentController(_repository, _mapping, _validationService);

            // Act
            var result = await controller.Update(_singleCommentDTO); // Call the Update method of the controller

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify OkObjectResult
            Assert.Equal(200, okResult.StatusCode); // Verify status code
        }
    }
}
