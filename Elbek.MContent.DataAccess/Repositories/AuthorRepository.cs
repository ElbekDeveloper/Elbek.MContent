using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories {
  public interface IAuthorRepository : IRepository<Author> {
    Task<Author> GetAuthorByName(string name);
  }
  public class AuthorRepository : BaseRepository<Author>, IAuthorRepository {
    public AuthorRepository(MContentContext dbContext) : base(dbContext) {}

    public async Task<Author> GetAuthorByName(string name) {
      /// TODO 7 добавь .AsNoTracking(), обычно все гет операции(данные которых
      /// ты потом не будешь изменять мы не трекаем)
      return await GenerateQuery().SingleOrDefaultAsync(i => i.Name == name);
    }
  }
}
