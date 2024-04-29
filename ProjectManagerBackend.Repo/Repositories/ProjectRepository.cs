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

        public async Task<UserDetail> GetAllProjectDashboards(int userId)
        {
            return await _context.UserDetails.Where(u => u.Id == userId)
                .Include(x => x.Projects)
                .ThenInclude(x => x.ProjectCategory)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetOwnerName(int ownerId)
        {
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == ownerId);
            return result.Username;
        }
    }
}
