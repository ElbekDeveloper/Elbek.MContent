using Elbek.MContent.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    /// Todo 5 совсем необязательно выносить интерфейс в отдельный файл, можно интерфейс описать в BaseRepository

    public interface IRepository<TModel> where TModel : BaseEntity
    {
        IQueryable<TModel> Query();
        Task<TModel> GetByIdAsync(Guid id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> AddAsync(TModel entity);
        Task<TModel> UpdateAsync(TModel entity);
        Task<TModel> DeleteAsync(TModel entity);
    }
}
