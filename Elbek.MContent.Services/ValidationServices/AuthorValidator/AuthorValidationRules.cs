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

    public string ValidateAuthorWasFound(Guid id, Author author) {
      return (author == null) ? $"Author with Id {id} not found" : string.Empty;
    }

    public string ValidateIfIdsAreSame(Guid idFromRoute, Guid idFromBody) {
      return (idFromRoute != idFromBody)
                 ? $"Id supplied in route '{idFromRoute}' doesn't correspond to that of body '{idFromBody}'"
                 : string.Empty;
    }

    public string ValidateUniqueAuthorId(Author authorWithUniqueId) {
      return (authorWithUniqueId != null)
                 ? $"Author with Id '{authorWithUniqueId.Id}' already exists"
                 : string.Empty;
    }

    public string ValidateUniqueAuthorName(Author authorWithUniqueName) {
      return authorWithUniqueName !=
             null ? $"Author  '{authorWithUniqueName.Name}' already exists"
          : string.Empty;
    }
  }
}
