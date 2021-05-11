using Elbek.MContent.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IRepository<TModel> where TModel : BaseEntity
    {
        IQueryable<TModel> GenerateQuery();
        Task<TModel> GetByIdAsync(Guid id);
        Task<IEnumerable<TModel>> GetAllAsync();
    }
}
