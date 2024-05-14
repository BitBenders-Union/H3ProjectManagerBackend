
namespace ProjectManagerBackend.Test.Repositories
{
    public class DepartmentRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;


        public DepartmentRepositoryTest()
        {        
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockLocation").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Departments.Add(new Department { Id = 1, Name = "Test Department 1" });
            _context.Departments.Add(new Department { Id = 2, Name = "Test Department 2" });
            _context.Departments.Add(new Department { Id = 3, Name = "Test Department 3" });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllDepartments_ReturnList()
        {
            // Arrange
            GenericRepository<Department> repository = new(_context);

            // Act
            ICollection<Department> departmentList = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, departmentList.Count);
        }

        [Fact]
        public async Task GetDepartmentById_ReturnOneDepartment()
        {
            // Arrange
            GenericRepository<Department> repository = new(_context);

            // Act
            Department department = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, department.Id);

            // Assert 2 - Testing for none existent ID
            // Assert.ThrowsAsync verifies if GetByIdAsync(99) throws an Exception, unit-test will fail it does not throw an exception.
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateDepartment_ReturnDepartment()
        {
            Department department = new Department { Id = 50, Name = "Test Department 50" };

            // Arrange
            GenericRepository<Department> repository = new(_context);

            // Act
            Department returnDepartment = await repository.CreateAsync(department);

            // Assert
            Assert.Equal(returnDepartment, department);
        }

        [Fact]
        public async Task DeleteDepartment_ReturnTrue()
        {
            // Arrange
            GenericRepository<Department> repository = new(_context);

            // Act
            bool result = await repository.DeleteAsync(1); // Assuming Id 1 does exist 
            bool falseResult = await repository.DeleteAsync(99); // Assuming ID 99 doesn't exist

            // Assert
            Assert.True(result); // Assert deletion of existing entity            
            Assert.False(falseResult); // Assert deletion of non-existing entity            
        }

        [Fact]
        public async Task UpdateDepartment_ReturnOkResult()
        {
            // Arrange
            GenericRepository<Department> repository = new(_context);

            Department department = await repository.GetByIdAsync(1);
            department.Name = "Test Location 1 updated";

            // Act
            var result = await repository.UpdateAsync(department);

            //Assert
            Assert.True(result);
        }

    }
}
