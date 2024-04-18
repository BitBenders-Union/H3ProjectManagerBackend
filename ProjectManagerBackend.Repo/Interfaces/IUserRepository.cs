using ProjectManagerBackend.Repo.DTOs;
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
        public Task<UserDetail> CreateUserAsync(UserDetail userDetail);
        public Task<bool> AccountExist(string username, byte[] passwordhash);
        public UserDetail GetUserDetail(string userName);
        public Task<bool> UpdateUser(UserDetail user);
        public Task<bool> Save();


    }
}
