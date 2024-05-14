
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ProjectManagerBackend.Test.Repositories
{
    public  class ProjectTaskRepositoryTest
    {
        DbContextOptions<DataContext> options;

        DataContext _context;

        GenericRepository<Project> _repository;
        public ProjectTaskRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "MockProjectTask").Options;

            _context = new DataContext(options);
            _context.Database.EnsureDeleted();

            _context.ProjectTasks.Add(new ProjectTask { Id = 1, Name = "Test ProjectTask 1", Description = "Test Description 1" });
            _context.ProjectTasks.Add(new ProjectTask { Id = 2, Name = "Test ProjectTask 2", Description = "Test Description 2" });
            _context.ProjectTasks.Add(new ProjectTask { Id = 3, Name = "Test ProjectTask 3", Description = "Test Description 3" });

            _context.SaveChanges();
        }
    }
}
