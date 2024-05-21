using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {

            _context = context;

        }

        public async Task CreateManyToMany(int projectId, int userId)
        {
            await _context.ProjectUserDetails.AddAsync(new ProjectUserDetail
            {
                ProjectId = projectId,
                UserDetailId = userId
            });
            if (_context.SaveChanges() > 0)
                return;
            throw new Exception("Failed to create many to many relationship");
        }

        public async Task<Project> Get(int id)
        {
            var result = await _context.Projects
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectCategory)
                .Include(x => x.Priority)
                .Include(x => x.Client)
                .Include(x => x.ProjectDepartment)
                    .ThenInclude(x => x.Department)
                .Include(x => x.ProjectUserDetail)
                    .ThenInclude(x => x.UserDetail)
                .Include(x => x.ProjectTasks)
                    .ThenInclude(x => x.Priority)
                .Include(x => x.ProjectTasks)
                    .ThenInclude(x => x.ProjectTaskCategory)
                .Include(x => x.ProjectTasks)
                    .ThenInclude(x => x.Status)
                .Include(x => x.ProjectTasks)
                    .ThenInclude(x => x.ProjectTaskUserDetail)
                        .ThenInclude(x => x.UserDetail)
                .Include(x => x.ProjectTasks)
                    .ThenInclude(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == id) ?? null;

            return result;
        }

        /// <summary>
        /// Returns a list of projects to be displayed in the project-dashbord, for a given user
        /// </summary>
        public async Task<List<Project>> GetAllProjectDashboards(int userId)
        {
            var result = await _context.Projects
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectCategory)
                .Include(x => x.Priority)
                .Include(x => x.Client)
                .Include(x => x.ProjectDepartment)
                    .ThenInclude(x => x.Department)
                .Include(x => x.ProjectUserDetail)
                    .ThenInclude(x => x.UserDetail)
                .Where(x => x.ProjectUserDetail.Any(x => x.UserDetailId == userId))
                .ToListAsync() ?? null;

            return result;
        }

        public async Task<Project> GetProject(int id)
        {
            var result = await _context.Projects.Include(x => x.ProjectStatus)
                .Include(x => x.ProjectCategory)
                .Include(x => x.Priority)
                .Include(x => x.Client)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectDepartment)
                    .ThenInclude(x => x.Department)
                .Include(x => x.ProjectUserDetail)
                    .ThenInclude(x => x.UserDetail)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<string> GetOwnerName(int ownerId)
        {
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == ownerId);
            return result.Username;
        }

        public bool UpdateProject(Project updatedProject)
        {
            //var existingProject =  _context.Projects
            //    .Include(p => p.ProjectTasks)
            //    .Include(p => p.ProjectDepartment)
            //    .Include(p => p.ProjectUserDetail)
            //    .FirstOrDefault(p => p.Id == updatedProject.Id);

            //if (existingProject == null)
            //{
            //    throw new KeyNotFoundException("Project not found");
            //}

            //// Update scalar properties
            //existingProject.Name = updatedProject.Name;
            //existingProject.StartDate = updatedProject.StartDate;
            //existingProject.EndDate = updatedProject.EndDate;
            //existingProject.ProjectStatus = updatedProject.ProjectStatus;
            //existingProject.ProjectCategory = updatedProject.ProjectCategory;
            //existingProject.Priority = updatedProject.Priority;
            //existingProject.Client = updatedProject.Client;
            //existingProject.Owner = updatedProject.Owner;

            //// Update ProjectTasks
            //_context.Entry(existingProject).Collection(p => p.ProjectTasks).Load();
            //existingProject.ProjectTasks.Clear();
            //existingProject.ProjectTasks.AddRange(updatedProject.ProjectTasks);

            //// Update ProjectDepartment
            //_context.Entry(existingProject).Collection(p => p.ProjectDepartment).Load();
            //existingProject.ProjectDepartment.Clear();
            //existingProject.ProjectDepartment.AddRange(updatedProject.ProjectDepartment);

            //// Update ProjectUserDetail
            //_context.Entry(existingProject).Collection(p => p.ProjectUserDetail).Load();
            //existingProject.ProjectUserDetail.Clear();
            //existingProject.ProjectUserDetail.AddRange(updatedProject.ProjectUserDetail);

            // Check if the project already exists
            var existingProject = _context.Projects
                .Include(p => p.ProjectDepartment)
                .Include(p => p.ProjectUserDetail)
                .FirstOrDefault(p => p.Id == updatedProject.Id);

            if (existingProject != null)
            {
                // Update existing project
                existingProject.Name = updatedProject.Name;
                existingProject.StartDate = updatedProject.StartDate;
                existingProject.EndDate = updatedProject.EndDate;
                existingProject.Owner = updatedProject.Owner;
                existingProject.ProjectStatus = updatedProject.ProjectStatus;
                existingProject.ProjectCategory = updatedProject.ProjectCategory;
                existingProject.Priority = updatedProject.Priority;
                existingProject.Client = updatedProject.Client;

                // Update ProjectDepartments
                existingProject.ProjectDepartment.Clear();
                foreach (var department in updatedProject.ProjectDepartment)
                {
                    existingProject.ProjectDepartment.Add(department);
                }

                // Update ProjectUserDetails
                existingProject.ProjectUserDetail.Clear();
                foreach (var userDetail in updatedProject.ProjectUserDetail)
                {
                    existingProject.ProjectUserDetail.Add(userDetail);
                }

                _context.Projects.Update(existingProject);
            }
            else
            {
                // Insert new project
                _context.Projects.Add(updatedProject);
            }

            // Save changes
            return  _context.SaveChanges() > 0;
        }

        public bool UpdatePD(List<ProjectDepartment> pd)
        {
            if(pd.Count == 0)
            {
                return true;
            }

            foreach (var item in pd)
            {
                _context.ProjectDepartments.Add(item);
            }
            return _context.SaveChanges() > 0;

        }


        public bool DeletePD(List<ProjectDepartment> pd)
        {
            var id = pd[0].ProjectId;
            var projectDepartments = _context.ProjectDepartments.Where(x => x.ProjectId == id).ToList();
            foreach (var item in projectDepartments)
            {
                _context.ProjectDepartments.Entry(item).State = EntityState.Deleted;
                _context.ProjectDepartments.Remove(item);
            }
            _context.SaveChanges();
            
            var result = _context.ProjectDepartments.Where(x => x.ProjectId == id).Any();
            return !result;
        }


        public bool UpdatePUD(List<ProjectUserDetail> pud)
        {
            if (pud.Count == 0)
            {
                return true;
            }

            foreach (var item in pud)
            {
                _context.ProjectUserDetails.Add(item);
            }
            return _context.SaveChanges() > 0;

        }


        public bool DeletePUD(List<ProjectUserDetail> pud)
        {
            var id = pud[0].ProjectId;
            var projectUserDetails = _context.ProjectUserDetails.Where(x => x.ProjectId == id).ToList();
            foreach (var item in projectUserDetails)
            {
                _context.ProjectUserDetails.Entry(item).State = EntityState.Deleted;
                _context.ProjectUserDetails.Remove(item);
            }
             _context.SaveChanges();

            var result = _context.ProjectUserDetails.Where(x => x.ProjectId == id).Any();
            return !result;
        }
    }
}


