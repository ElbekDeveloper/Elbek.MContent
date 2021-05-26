using System;
using System.Collections.Generic;
using System.Linq;
using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetByName(string name);
        Task<ICollection<Author>> Get(List<Guid> ids, List<string> names);
    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(MContentContext dbContext) : base(dbContext)
        {
        }

       
        public async Task<Author> GetByName(string name)
        {
            return await Query().AsNoTracking().SingleOrDefaultAsync(i => i.Name == name);
        }

        public async Task<ICollection<Author>> Get(List<Guid> ids, List<string> names)
        {
            return await Query().AsNoTracking().Where(x => ids.Contains(x.Id) || names.Contains(x.Name)).ToListAsync();
        }
    }
}
