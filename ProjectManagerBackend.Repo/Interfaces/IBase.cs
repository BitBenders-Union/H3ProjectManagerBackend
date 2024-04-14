using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
<<<<<<< Updated upstream:ProjectManagerBackend.Repo/Interfaces/ICrudRepository.cs
    public interface ICrudInterface<TEntity> where TEntity : class
    {
        public Task<ICollection<TEntity>> GetAll();
        public Task<TEntity> Create(TEntity entity);
=======
    public interface IBase<T> where T : class
    {
        public int Id { get; set; }
>>>>>>> Stashed changes:ProjectManagerBackend.Repo/Interfaces/IBase.cs

    }
}
