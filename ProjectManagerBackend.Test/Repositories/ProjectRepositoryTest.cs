using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using ProjectManagerBackend.Repo.Repositories;
using ProjectManagerBackend.Repo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Test.Repositories
{
    public class ProjectRepositoryTest
    {
        DbContextOptions<DataContext> options;
        DataContext _context;
        ProjectRepository _repository;
        GenericRepository<Project> _genericRepository;
        public ProjectRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "DummyDatabase").Options;

            _context = new DataContext(options);
            
            _repository = new ProjectRepository(_context);

            _genericRepository = new GenericRepository<Project>(_context);

            _context.Database.EnsureDeleted();

            ProjectStatus ps = new()
            {
                Name = "Test Status"
            };
            _context.ProjectStatus.Add(ps);

            ProjectCategory pc = new()
            {
                Name = "Test Category"
            };
            _context.ProjectCategories.Add(pc);

            Priority pri = new()
            {
                Name = "High",
                Level = 3
            };
            _context.Priorities.Add(pri);


            HashingService hashingService = new();
            var salt = hashingService.GenerateSalt();
            UserDetail usr = new()
            {
                FirstName = "a",
                LastName = "b",
                Username = "admin",
                PasswordSalt = salt,
                PasswordHash = hashingService.PasswordHashing("123", salt),
                IsActive = true,
                CreatedDate = DateTime.Now

            };

            _context.UserDetails.Add(usr);


            //_context.SaveChanges();


            Project project = new()
            {
                Name = "Project 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ProjectStatus = _context.ProjectStatus.FirstOrDefault(x => x.Name == ps.Name),
                ProjectTasks = new List<ProjectTask>(),
                ProjectCategory = _context.ProjectCategories.FirstOrDefault(x => x.Name == pc.Name),
                Priority = _context.Priorities.FirstOrDefault(x => x.Level == pri.Level),
                ProjectDepartment = new List<ProjectDepartment>(),
                ProjectUserDetail = new List<ProjectUserDetail>(),
                Owner = "Admin"
            };
            _context.Projects.Add(project);


            _context.SaveChanges();


            ProjectUserDetail pud = new()
            {
                ProjectId = _context.Projects.FirstOrDefault(x => x.Name == project.Name).Id,
                UserDetailId = _context.UserDetails.FirstOrDefault(x => x.Username == usr.Username).Id
            };

            _context.ProjectUserDetails.Add(pud);

            _context.SaveChanges();


            var pudR = _context.ProjectUserDetails.FirstOrDefault();
            var projectR = _context.Projects.FirstOrDefault();



            List<ProjectUserDetail> projectUserDetails = [pudR];
            projectR.ProjectUserDetail = projectUserDetails;
            _context.Projects.Update(projectR);

            _context.SaveChanges();


        }

        [Fact]
        public void CreateProject_ShouldReturnBool()
        {

            Project project = new()
            {
                Name = "Project 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Owner = "Admin"
            };
            _context.Projects.Add(project);
            var saved =  _context.SaveChanges() > 0;

            Assert.True(saved);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfProject_WhenProjectsAndUserExist()
        {
            // Arrange
            var expectedCount = 1;
            var usr = _context.UserDetails.FirstOrDefault();

            // Act

            
            var result = await _repository.GetAllProjectDashboards(usr.Id);

            // Assert

            Assert.Equal(expectedCount, result.Count);

        }

        [Fact]
        public async void DeleteProject_ShouldReturnTrue_WhenProjectExist()
        {
            // Arrange

            Project project = new()
            {
                Name = "Project To Delete",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Owner = "delete"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

            var projectToDelete = _context.Projects.FirstOrDefault(x => x.Name == project.Name);

            // Act
            var isDeleted = await _genericRepository.DeleteAsync(projectToDelete.Id);

            // Assert

            Assert.True(isDeleted);

        }

        [Fact]
        public async void DeleteProject_ShouldReturnFalse_WhenProjectDoesNotExist()
        {
            // Arrange


            var projectToDelete = -1;

            // Act
            var isDeleted = await _genericRepository.DeleteAsync(projectToDelete);

            // Assert

            Assert.False(isDeleted);

        }

        [Fact]

        public async void GetOwnerName_ShouldReturnOwnerName_WhenOwnerExist()
        {
            // Arrange
            var expectedOwner = "admin";
            var usr = _context.UserDetails.FirstOrDefault();

            // Act
            var result = await _repository.GetOwnerName(usr.Id);

            // Assert
            Assert.Equal(expectedOwner, result);
        }

    }
}
