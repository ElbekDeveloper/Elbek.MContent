using Elbek.MContent.DataAccess.Data;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {

    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(MContentContext dbContext) : base(dbContext)
        {
        }
    }
}
