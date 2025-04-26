using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.Layer.interfaces
{
    public interface IGenearicRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task <TEntity>? GetAsync(int id);
        Task <IEnumerable<TEntity>> GetAllAsync();
    }
}
    