using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly DataContext _context;
        public ProjectTaskRepository(DataContext context)
        {
            _context = context;
        }
        public async Task CreateManyToMany(int taskId, int userId)
        {
            await _context.ProjectTaskUserDetails.AddAsync(new ProjectTaskUserDetail
            {
                ProjectTaskId = taskId,
                UserDetailId = userId
            });
            if (_context.SaveChanges() > 0)
                return;
            throw new Exception("Failed to create many to many relationship");
        }
    }
}
