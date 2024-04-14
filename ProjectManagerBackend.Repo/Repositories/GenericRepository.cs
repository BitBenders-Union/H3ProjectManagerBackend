using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.Interfaces;
<<<<<<< Updated upstream
using ProjectManagerBackend.Repo.Models;
=======
>>>>>>> Stashed changes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Repositories
{
<<<<<<< Updated upstream
    public class GenericRepository<TEntity> where TEntity : class,
        ICrudInterface<TEntity>
    {
        private readonly DataContext _context;
=======
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

>>>>>>> Stashed changes
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
<<<<<<< Updated upstream

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            var result = await _context.SaveChangesAsync() > 0 ? entity : throw new Exception("Unable to create the entity.");

            return result;
        }


=======
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
>>>>>>> Stashed changes
    }
}
