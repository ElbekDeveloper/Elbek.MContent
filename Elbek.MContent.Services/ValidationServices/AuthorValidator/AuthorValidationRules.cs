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
        List<string> ValidateMatchingAuthors(IEnumerable<Author> authors,ICollection<AuthorDto> authorDtos);

    }
    public class AuthorValidationRules : GenericValidationRules, IAuthorValidationRules

    {
        public string ValidateAuthorWasFound(Guid id, Author author)
        {
            return (author == null) ? ValidationErrorMessages.EntityNotFound<Author>(id) : string.Empty;
        }
        public string ValidateAuthorWasFound(string name, Author author)
        {
            return (author == null) ? ValidationErrorMessages.AuthorNotFound(name) : string.Empty;
        }
        public string ValidateAgainstDuplicates(ICollection<AuthorDto> authorDtos)
        {
            var duplicates = authorDtos.GroupBy(a => a.Id)
                                                          .Where(g => g.Count() > 1)
                                                          .SelectMany(g => g);

            return duplicates.Any() ? ValidationErrorMessages.DuplicateAuthors() : string.Empty;
        }

        public string ValidateIfAnyAuthorExists(ICollection<AuthorDto> authorDtos)
        {
            return !authorDtos.Any() ? ValidationErrorMessages.EntityRequired<Author>() : string.Empty;
        }

        public string ValidateIfIdsAreSame(Guid idFromRoute, Guid idFromBody)
        {
            return (idFromRoute != idFromBody)
                ?
               ValidationErrorMessages.UnmatchingIds(idFromRoute, idFromBody)
                :
                string.Empty;
        }

        public string ValidateUniqueAuthorId(Author authorWithUniqueId)
        {
            return (authorWithUniqueId != null) ? ValidationErrorMessages.EntityWithIdExists<Author>(authorWithUniqueId.Id) : string.Empty;
        }

        public string ValidateUniqueAuthorName(Author authorWithUniqueName)
        {
            return authorWithUniqueName != null ? ValidationErrorMessages.AuthorExists(authorWithUniqueName.Name) : string.Empty;
        }

        public List<string> ValidateMatchingAuthors(IEnumerable<Author> authors,ICollection<AuthorDto> authorDtos)
        {
            var result = new List<string>();

            var notExistingById = authorDtos.Where(x => !authors.Select(z => z.Id).Contains(x.Id)).ToList();
            var notExistingByName = authorDtos.Where(x => !authors.Select(z => z.Name).Contains(x.Name)).ToList();

            if (notExistingById.Any() || notExistingByName.Any())
            {
                result.AddRange(notExistingById.Select(x => ValidationErrorMessages.Author.AuthorNotFoundId(x.Id)));
                result.AddRange(notExistingByName.Select(x => ValidationErrorMessages.Author.AuthorNotFoundName(x.Name)));
            }

            return result;

            //if (authors.Count() != authorDtos.Count())
            //{
            //    var ids = string.Join(", ", authorDtos.Select(a => a.Id));
            //    var names = string.Join(", ", authorDtos.Select(a => a.Name));

            //    return authorDtos != null ? ValidationErrorMessages.AuthorNotFound(ids, names) : string.Empty;
            //}
            //return string.Empty;
        }
    }
}
