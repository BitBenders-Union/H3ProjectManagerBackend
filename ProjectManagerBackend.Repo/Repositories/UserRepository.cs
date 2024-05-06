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
            await _context.AddAsync(userDetail);
            await _context.SaveChangesAsync();
            return userDetail;
        }

        public async Task<bool> AccountExist(string username, byte[] passwordhash)
        {
            bool user = await _context.UserDetails.AnyAsync(x => x.Username.ToLower() == username.Trim().ToLower());
            bool pwd = await _context.UserDetails.AnyAsync(x => x.PasswordHash == passwordhash);

            if (!user || !pwd)
                return false;
            return true;
        }

        public async Task<UserDetail> GetUserDetail(string userName)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(x => x.Username == userName);
        }

        public async Task<bool> Save()
        {
           var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<UserDetail> GetUserDetail(int id)
        {
            return await _context.UserDetails.Include(x => x.Department).Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
