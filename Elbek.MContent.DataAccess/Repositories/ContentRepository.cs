using Elbek.MContent.DataAccess.Data;

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
    }
}
