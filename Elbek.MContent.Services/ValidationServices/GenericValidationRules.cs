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
        /// <summary>
        /// returns error message if id is default. Otherwise, returns empty string
        /// </summary>
        /// <param name="id">Guid to be checked</param>
        /// <returns></returns>
        public string ValidateGuidIfDefault(Guid id)
        {
            return (id == Guid.Empty) ? ValidationErrorMessages.FieldRequired(id) : string.Empty;
        }

        public string ValidateIfNullOrEmpty(string field)
        {
            return (string.IsNullOrWhiteSpace(field) || string.IsNullOrEmpty(field)) ? ValidationErrorMessages.FieldRequired(field) : string.Empty;
        }
    }
}
