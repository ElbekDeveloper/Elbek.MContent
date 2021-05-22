using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IContentRepository: IRepository<Content>
    {
        Task<IList<Content>> GetByTypeAsync(int type);
        Task<Content> GetContentByTitle(string title, int type);

    }
    public class ContentRepository : BaseRepository<Content>, IContentRepository
    {
        public ContentRepository(MContentContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Content>> GetByTypeAsync(int type)
        {// add .AsNoTracking()
            return await Query().Where(c => (int)c.Type == type).ToListAsync();
        }

        //rename to GetByTitle
        public async Task<Content> GetContentByTitle(string title, int type)
        {
            return await Query().AsNoTracking().SingleOrDefaultAsync(c => c.Title == title && (int)c.Type == type);
        }

        public override IQueryable<Content> Query()
        {
            return base.Query().Include(c => c.ContentAuthors).ThenInclude(ca => ca.Author).AsQueryable() ;
        }
    }
}
