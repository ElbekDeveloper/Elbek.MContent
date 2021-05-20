using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IContentRepository: IRepository<Content>
    {

    }
    public class ContentRepository : BaseRepository<Content>, IContentRepository
    {
        public ContentRepository(MContentContext dbContext) : base(dbContext)
        {
        }
        public async override Task<IList<Content>> GetAllAsync()
        {
           return await Query().Include(c => c.ContentAuthors).ThenInclude(ca => ca.Author).ToListAsync();
        }
    }
}
