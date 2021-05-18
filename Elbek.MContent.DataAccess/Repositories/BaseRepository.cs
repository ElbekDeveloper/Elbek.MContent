using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IRepository<TModel> where TModel : BaseEntity
    {
        IQueryable<TModel> Query();
        Task<TModel> GetByIdAsync(Guid id);
        Task<IList<TModel>> GetAllAsync();
        Task<TModel> AddAsync(TModel entity);
        Task<TModel> UpdateAsync(TModel entity);
        Task<TModel> DeleteAsync(TModel entity);
    }

    public abstract class BaseRepository<TModel> : IRepository<TModel> where TModel : BaseEntity
    {
        protected readonly MContentContext _dbContext;

        public BaseRepository(MContentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TModel> Query()
        {
            return _dbContext.Set<TModel>().AsQueryable();
        }

        public virtual async Task<IList<TModel>> GetAllAsync()
        {
            return await Query().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await Query().AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
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
