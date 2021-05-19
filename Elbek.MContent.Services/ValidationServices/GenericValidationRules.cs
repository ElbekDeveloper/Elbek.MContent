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
            if (id == Guid.Empty)
            {
                return $"Field {id} is required. Default values are not accepted";
            }
            return string.Empty;
        }

        public string ValidateIfNullOrEmpty(string field)
        {
            if (string.IsNullOrWhiteSpace(field) || string.IsNullOrEmpty(field))
            {
                return $"Field {nameof(field)} is required";
            }
            return string.Empty;
        }

    }
}
