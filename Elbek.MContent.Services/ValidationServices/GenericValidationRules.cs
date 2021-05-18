using System;
using System.ComponentModel.DataAnnotations;

namespace Elbek.MContent.Services.ValidationServices
{
    public interface IGenericValidationRules
    {
        string ValidateIfNullOrEmpty(string field);
        string ValidateGuidIfDefault(Guid id);
        string ValidateIfUnique(string comparerOne, string comparerTwo);
        string ValidateIfUnique(Guid comparerOne, Guid comparerTwo);
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

        public string ValidateIfUnique(string comparerOne, string comparerTwo)
        {
            if (string.Compare(comparerOne.ToLower(), comparerTwo.ToLower()) == 0)
            {
                return $"Author with name '{comparerOne}' already exists";
            }
            return string.Empty;
        }

        public string ValidateIfUnique(Guid comparerOne, Guid comparerTwo)
        {
            if (comparerOne == comparerTwo)
            {
                return $"Author with Id {comparerOne} already exists";
            }
            return string.Empty;
        }
    }
}
