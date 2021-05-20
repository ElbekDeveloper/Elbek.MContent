using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.CoreServices
{
    public interface IContentService : ICoreService<ContentDto>
    {

    }
    public class ContentService : IContentService
    {
        public Task<MContentResult<ContentDto>> AddAsync(ContentDto TModel)
        {
            throw new NotImplementedException();
        }

        public Task<MContentResult<IList<ContentDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MContentResult<ContentDto>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MContentResult<ContentDto>> UpdateAsync(Guid id, ContentDto TModel)
        {
            throw new NotImplementedException();
        }
    }
}
