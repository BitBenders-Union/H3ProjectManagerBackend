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
        private readonly DataContext _context;

        public UserRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<bool> CheckUser(string username)
        {
            return await _context.UserDetails.AnyAsync(x => x.Username.Trim().ToLower() == username.Trim().ToLower());
        }

        public async Task<bool> UpdateUser(UserDetail user)
        {
            _context.UserDetails.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserDetail> CreateUserAsync(UserDetail userDetail)
        {
            await dataContext.AddAsync(userDetail);
            await dataContext.SaveChangesAsync();
            return userDetail;
        }
    }
}
