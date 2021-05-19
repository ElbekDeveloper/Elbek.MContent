using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetAuthorByName(string name);
    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(MContentContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<Author> Query()
        {
            return base.Query().Include(ac => ac.ContentAuthors).ThenInclude(c => c.Content);
        }
        public async Task<Author> GetAuthorByName(string name)
        {
            return await Query().AsNoTracking().SingleOrDefaultAsync(i => i.Name == name);
        }
    }
}
