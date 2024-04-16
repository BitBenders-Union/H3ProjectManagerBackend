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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<bool> CheckUser(string username)
        {
            return await dataContext.UserDetails.AnyAsync(x => x.Username.Trim().ToLower() == username.Trim().ToLower());
        }

        public async Task<UserDetail> CreateUserAsync(UserDetail userDetail)
        {
            await dataContext.AddAsync(userDetail);
            await dataContext.SaveChangesAsync();
            return userDetail;
        }
    }
}
