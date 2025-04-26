using Data.Access.Layer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.Layer.Repositories
{
    public class GenericRepository<TEntity> : IGenearicRepository<TEntity> where TEntity : class
    {
        private DataContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        public GenericRepository(DataContext dataContext)
        {
            _dbContext = dataContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

		public void Delete(TEntity entity) => _dbSet.Remove(entity);
        public void Update(TEntity entity) => _dbSet.Update(entity);
            
        public async Task <TEntity?> GetAsync(int id) => await _dbSet.FindAsync(id);
        public async Task <IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();


    }
}
