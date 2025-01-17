﻿using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Elbek.MContent.DataAccess.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetByName(string name);
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
    }
}
