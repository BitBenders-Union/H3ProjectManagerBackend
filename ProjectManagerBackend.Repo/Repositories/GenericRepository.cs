using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        // Create
        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
                return entity;

            throw new Exception("Could not Create Entity");
        }

        // Read
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id) ?? throw new Exception("No Entity with Id Found");
        }

        // Update
        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // Delete

        public async Task<bool> DeleteAsync(int id)
        {
            // find entity with the id provided
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity == null)
                return false;

            // remove and save to db
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }




    }
}
