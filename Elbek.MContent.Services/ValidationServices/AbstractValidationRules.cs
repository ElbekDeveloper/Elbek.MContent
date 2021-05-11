using Elbek.MContent.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices
{
    public abstract class AbstractValidationRules<T> where T : class
    {
        protected string ValidateIfNullOrEmpty(string field)
        {
            if (string.IsNullOrWhiteSpace(field) || string.IsNullOrEmpty(field))
            {
                return $"Field {field} is required";
            }
            return string.Empty;
        }

        protected string ValidateIfUnique(T type)
        {
            if (!type.Equals(null))
            {
                return $"{nameof(T)} already exists";
            }
            return string.Empty;
        }

    }
}
