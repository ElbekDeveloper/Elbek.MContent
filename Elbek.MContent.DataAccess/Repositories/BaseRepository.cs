using Elbek.MContent.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public class BaseRepository<TModel> : IRepository<TModel> where TModel : BaseEntity
    {
        public IQueryable<TModel> GenerateQuery()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
