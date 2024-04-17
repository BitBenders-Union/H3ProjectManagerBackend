using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CheckUser(string username);
        public Task<bool> UpdateUser(UserDetail user);
    }
}
