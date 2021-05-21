using Elbek.MContent.Services.Models;
using System;

namespace Elbek.MContent.Services.ValidationServices.ContentValidator
{
    public interface IContentValidationRules: IGenericValidationRules
    {
        string ValidateTypeRange(int type);
    }
    public class ContentValidationRules : GenericValidationRules, IContentValidationRules
    {
        public string ValidateTypeRange(int type)
        {
            var types = Enum.GetNames(typeof(ContentTypeDto));
            if (type > types.Length - 1
                || type < 0)
            {
                return $"Type '{type}' should have one of the following values: {string.Join(", ", types)}";
            }
            return string.Empty;
        }
    }
}
