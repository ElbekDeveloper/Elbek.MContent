using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator {
  // https://github.com/aspnet/samples/blob/main/samples/aspnet/WebApi/ValidationSample/ValidationSample/Validators/NonNegativeAttribute.cs
  public interface IAuthorValidationRules : IGenericValidationRules {
    string ValidateAuthorWasFound(Guid id, Author author);
    string ValidateUniqueAuthorName(Author authorWithUniqueName);
    string ValidateUniqueAuthorId(Author authorWithUniqueId);
    string ValidateIfIdsAreSame(Guid idFromRoute, Guid idFromBody);
  }
  public class AuthorValidationRules : GenericValidationRules,
                                       IAuthorValidationRules {

    /// TODO 7 используй (сщтвшешщт)?: здесь везде и в базовом классе
    public string ValidateAuthorWasFound(Guid id, Author author) {
      if (author == null) {
        return $"Author with Id {id} not found";
      }
      return string.Empty;
    }

    public string ValidateIfIdsAreSame(Guid idFromRoute, Guid idFromBody) {
      if (idFromRoute != idFromBody) {
        return $"Id supplied in route '{idFromRoute}' doesn't correspond to that of body '{idFromBody}'";
      }
      return string.Empty;
    }

    public string ValidateUniqueAuthorId(Author authorWithUniqueId) {

      if (authorWithUniqueId != null) {
        return $"Author with Id '{authorWithUniqueId.Id}' already exists";
      }
      return string.Empty;
    }

    public string ValidateUniqueAuthorName(Author authorWithUniqueName) {

      if (authorWithUniqueName != null) {
        return $"Author  '{authorWithUniqueName.Name}' already exists";
      }
      return string.Empty;
    }
  }
}
