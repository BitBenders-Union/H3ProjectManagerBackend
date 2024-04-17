using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IGenericRepository<T> 
        where T : class
    {
        public Task<T> CreateAsync(T entity);
        public Task<ICollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);

    }
}
