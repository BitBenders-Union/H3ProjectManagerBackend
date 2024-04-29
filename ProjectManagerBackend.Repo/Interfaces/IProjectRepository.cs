using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IProjectRepository
    {
        public Task<UserDetail> GetAllProjectDashboards(int userId);
        public Task<string> GetOwnerName(int ownerId);
    }
}
