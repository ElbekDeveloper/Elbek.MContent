using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator
{
    //https://github.com/aspnet/samples/blob/main/samples/aspnet/WebApi/ValidationSample/ValidationSample/Validators/NonNegativeAttribute.cs
    public interface IAuthorValidationRules : IGenericValidationRules
    {
        string ValidateFoundAuthor(Guid id, Author author);
    }
    public class AuthorValidationRules : GenericValidationRules, IAuthorValidationRules
    {
        public string ValidateFoundAuthor(Guid id, Author author)
        {
            if (author == null)
            {
                return $"Author with Id {id} not found";
            }
            return string.Empty;
        }
    }
}
