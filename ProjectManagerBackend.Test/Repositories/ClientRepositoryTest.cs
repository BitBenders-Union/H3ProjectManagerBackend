

namespace ProjectManagerBackend.Test.Repositories
{
    public class ClientRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;

        public ClientRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockClient").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Clients.Add(new Client
            {
                Id = 1,
                Name = "Test Client 1",
                Description = "Test Description 1",
                Adress = "Test Address 1",
                Email = "Test Email 1"
            });
            
            _context.Clients.Add(new Client
            {
                Id = 2,
                Name = "Test Client 2",
                Description = "Test Description 2",
                Adress = "Test Address 2",
                Email = "Test Email 2"
            });

            _context.Clients.Add(new Client
            {
                Id = 3,
                Name = "Test Client 3",
                Description = "Test Description 3",
                Adress = "Test Address 3",
                Email = "Test Email 3"
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllClients_ReturnList()
        {
            // Arrange
            GenericRepository<Client> repository = new(_context);

            // Act
            ICollection<Client> returnedList = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, returnedList.Count);
        }

        [Fact]
        public async Task GetClientById_ReturnOneClient()
        {
            // Arrange
            GenericRepository<Client> repository = new(_context);

            // Act
            Client client = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, client.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateClient_ReturnClient()
        {
            Client client = new()
            {
                Name = "Test Client 50",
                Description = "Test Description 50",
                Adress = "Test Address 50",
                Email = "Test Email 50"
            };

            // Arrange
            GenericRepository<Client> repository = new(_context);

            // Act
            Client returnClient = await repository.CreateAsync(client);

            // Assert
            Assert.Equal(returnClient, client);
        }

        [Fact]
        public async Task DeleteClient_ReturnTrue()
        {
            Client client = new ()
            {
                Name = "Test Client 999",
                Description = "Test Description 999",
                Adress = "Test Address 999",
                Email = "Test Email 999"
            }; 
            _context.Clients.Add(client);
            _context.SaveChanges();

            // Arrange
            GenericRepository<Client> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(client.Id);
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateClient_ReturnOkResult()
        {
            // Arrange
            GenericRepository<Client> repository = new(_context);

            Client client = await repository.GetByIdAsync(1);
            client.Name = "Test Client 1 updated";

            // Act
            var result = await repository.UpdateAsync(client);

            //Assert
            Assert.True(result);
        }
    }
}
