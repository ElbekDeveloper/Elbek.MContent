using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.CoreServices
{
    public interface ICoreService<TModel> where TModel : class
    {
        Task<MContentResult<IList<TModel>>> GetAllAsync();
        Task<MContentResult<TModel>> GetByIdAsync(Guid id);
        Task<MContentResult<TModel>> AddAsync(TModel TModel);
        Task<MContentResult<TModel>> UpdateAsync(Guid id, TModel TModel);
    }
}
