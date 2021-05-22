using Elbek.MContent.Services.Models;
using System;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices
{
    public interface IValidationService<TModel> where TModel : class
    {
        Task<MContentValidationResult> ValidateUpdate(Guid id, TModel model);
        Task<MContentValidationResult> ValidateAdd(TModel model);
        Task<MContentValidationResult> ValidateGetById(Guid id);
        Task<MContentValidationResult> ValidateGetAll();
    }
}
