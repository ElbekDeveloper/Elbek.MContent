using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public abstract class BaseRepository<TModel> : IRepository<TModel> where TModel : BaseEntity
    {
        protected readonly MContentContext _dbContext;

        public BaseRepository(MContentContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// TODO 6 GenerateQuery - этот метод не генерирует запрос, он какбы предоставляет доступ к данным и служит для построения запросов.
        /// Лучше назвать его просто Query

        public virtual IQueryable<TModel> GenerateQuery()
        {
            return _dbContext.Set<TModel>().AsQueryable();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            /// AsNoTracking() - это хорошо, почитай про Tracking в EntityFramework
            return await GenerateQuery().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await GenerateQuery().AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<TModel> AddAsync(TModel entity)
        {
            await _dbContext.Set<TModel>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TModel> UpdateAsync(TModel entity)
        {
            var updatedEntity = _dbContext.Set<TModel>().Update(entity);
            updatedEntity.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TModel> DeleteAsync(TModel entity)
        {
            _dbContext.Set<TModel>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
