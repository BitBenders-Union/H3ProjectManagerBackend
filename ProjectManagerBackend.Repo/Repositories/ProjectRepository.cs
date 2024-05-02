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

        public async Task<List<Project>> GetAllProjectDashboards(int userId)
        {
            var result = await _context.Projects
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectCategory)
                .Include(x => x.Priority)
                .Include(x => x.Client)
                .Include(x => x.Departments)
                .Include(x => x.Users)
                .Where(x => x.Users.Any(x => x.Id == userId))
                .ToListAsync();

            return result;
        }

        public async Task<string> GetOwnerName(int ownerId)
        {
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == ownerId);
            return result.Username;
        }
    }
}
