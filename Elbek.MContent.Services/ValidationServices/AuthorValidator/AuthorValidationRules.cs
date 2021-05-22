using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator
{
    public interface IAuthorValidationRules : IGenericValidationRules
    {
        string ValidateAuthorWasFound(Guid id, Author author);
        string ValidateAuthorWasFound(string name, Author author);
        string ValidateUniqueAuthorName(Author authorWithUniqueName);
        string ValidateUniqueAuthorId(Author authorWithUniqueId);
        string ValidateIfIdsAreSame(Guid idFromRoute, Guid idFromBody);
        string ValidateIfAnyAuthorExists(ICollection<AuthorDto> authorDtos);
        string ValidateAgainstDuplicates(ICollection<AuthorDto> authorDtos);
        string ValidateUnmatchedAuthors(ICollection<AuthorDto> authorDtos);

    }
    public class AuthorValidationRules : GenericValidationRules, IAuthorValidationRules

    {

        public string ValidateAuthorWasFound(Guid id, Author author)
        {
            return (author == null) ? $"Author with Id {id} not found" : string.Empty;
        }
        public string ValidateAuthorWasFound(string name, Author author)
        {
            return (author == null) ? $"Author with Name {name} not found" : string.Empty;
        }
        public string ValidateAgainstDuplicates(ICollection<AuthorDto> authorDtos)
        {
            var duplicates = authorDtos.GroupBy(a => a.Id)
                                                          .Where(g => g.Count() > 1)
                                                          .SelectMany(g => g);

            return duplicates.Any() ? $"Authors cannot contain duplicates" : string.Empty;
        }



        public string ValidateIfAnyAuthorExists(ICollection<AuthorDto> authorDtos)
        {
            return !authorDtos.Any() ? $"At least, one author required" : string.Empty;
        }

        public string ValidateIfIdsAreSame(Guid idFromRoute, Guid idFromBody)
        {
            return (idFromRoute != idFromBody) 
                ?
                $"Id supplied in route '{idFromRoute}' doesn't correspond to that of body '{idFromBody}'"
                :
                string.Empty;
        }

        public string ValidateUniqueAuthorId(Author authorWithUniqueId)
        {
            return (authorWithUniqueId != null) ? $"Author with Id '{authorWithUniqueId.Id}' already exists" : string.Empty;
        }

        public string ValidateUniqueAuthorName(Author authorWithUniqueName)
        {
            return authorWithUniqueName != null ? $"Author  '{authorWithUniqueName.Name}' already exists" : string.Empty;
        }

        public string ValidateUnmatchedAuthors(ICollection<AuthorDto> authorDtos)
        {
            var ids = string.Join(", ", authorDtos.Select(a => a.Id));
            var names = string.Join(", ", authorDtos.Select(a => a.Name));

            return authorDtos != null ? $"Author with {ids} or {names} was not found" : string.Empty;
        }
    }
}
