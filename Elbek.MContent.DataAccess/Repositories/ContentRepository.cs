using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IContentRepository: IRepository<Content>
    {
        Task<IList<Content>> GetByType(ContentType? type);
        Task<Content> GetByTitleAndType(string title, ContentType? type);

    }
    public class ContentRepository : BaseRepository<Content>, IContentRepository
    {
        public ContentRepository(MContentContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Content>> GetByType(ContentType? type)
        {
            return await Query().AsNoTracking().Where(c => c.Type == type).ToListAsync();
        }

        public async Task<Content> GetByTitleAndType(string title, ContentType? type)
        {
            return await Query().AsNoTracking().SingleOrDefaultAsync(c => c.Title == title && c.Type == type);
        }

        public override IQueryable<Content> Query()
        {
            return base.Query().Include(c => c.ContentAuthors).ThenInclude(ca => ca.Author).AsQueryable() ;
        }
    }
}
