using System;
using System.ComponentModel.DataAnnotations;

namespace Elbek.MContent.Services.ValidationServices
{
    public interface IGenericValidationRules
    {
        string ValidateIfNullOrEmpty(string field);
        string ValidateGuidIfDefault(Guid id);
    }

    public abstract class GenericValidationRules : ValidationAttribute, IGenericValidationRules
    {
        public string ValidateGuidIfDefault(Guid id)
        {
            return (id == Guid.Empty) ? $"Field {id} is required. Default values are not accepted" : string.Empty;
        }

        public string ValidateIfNullOrEmpty(string field)
        {
            return (string.IsNullOrWhiteSpace(field) || string.IsNullOrEmpty(field)) ? $"Field {nameof(field)} is required" : string.Empty;
        }
    }
}
