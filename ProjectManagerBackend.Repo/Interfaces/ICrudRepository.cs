using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface ICrudInterface<TEntity> where TEntity : class
    {
        public Task<ICollection<TEntity>> GetAll();
        public Task<TEntity> Create(TEntity entity);

    }
}
