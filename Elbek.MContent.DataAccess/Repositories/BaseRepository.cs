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

        public virtual IQueryable<TModel> GenerateQuery()
        {
            return _dbContext.Set<TModel>().AsQueryable();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await GenerateQuery().ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await GenerateQuery().SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
