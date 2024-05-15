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

        public async Task<string> GetOwnerName(int ownerId)
        {
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == ownerId);
            return result.Username;
        }
    }
}
