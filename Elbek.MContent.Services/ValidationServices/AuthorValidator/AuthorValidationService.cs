using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator
{
public interface IAuthorValidationService
{
    MContentValidationResult ValidateUpdateAuthor(Guid id, AuthorDto authorDto, Author authorWithSimilarId, Author authorWithSimilarName);
    MContentValidationResult ValidateAddAuthor(AuthorDto authorDto, Author authorWithSimilarId, Author authorWithSimilarName);
    MContentValidationResult ValidateGetById(Guid id, Author author);
    MContentValidationResult ValidateGetAll(IList<Author> authors);
}

public class AuthorValidationService : IAuthorValidationService
{
    private readonly IAuthorValidationRules _rules;
    public MContentValidationResult ValidationResult {
        get;
        private set;
    } = new MContentValidationResult();

    public AuthorValidationService(IAuthorValidationRules rules)
    {
        _rules = rules;
    }

    public MContentValidationResult ValidateGetById(Guid id, Author author)
    {
        ValidationResult.Errors = new List<string>
        {
            _rules.ValidateIfNullOrEmpty(id.ToString()),
            _rules.ValidateGuidIfDefault(id),
            _rules.ValidateAuthorWasFound(id, author)
        }.Where(e => string.IsNullOrEmpty(e) == false).ToList();

        if (author == null)
        {
            ValidationResult.StatusCode = (int)StatusCodes.NotFound;
        }
        else if (ValidationResult.Errors.Any())
        {
            ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
        }
        return ValidationResult;
    }

    public MContentValidationResult ValidateUpdateAuthor(Guid id,AuthorDto authorDto, Author authorWithSimilarId, Author authorWithSimilarName)
    {
        ValidationResult.Errors = new List<string>
        {
            _rules.ValidateIfIdsAreSame(id, authorDto.Id),
            _rules.ValidateAuthorWasFound(id, authorWithSimilarId),
            _rules.ValidateUniqueAuthorName(authorWithSimilarName),
            _rules.ValidateIfNullOrEmpty(authorDto.Id.ToString()),
            _rules.ValidateGuidIfDefault(authorDto.Id),
            _rules.ValidateIfNullOrEmpty(authorDto.Name)
        }.Where(e => !string.IsNullOrEmpty(e)).ToList();

        if (authorWithSimilarId == null)
        {
            ValidationResult.StatusCode = (int)StatusCodes.NotFound;
        }
        else if (ValidationResult.Errors.Any())
        {
            ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
        }
        return ValidationResult;
    }

    public MContentValidationResult ValidateGetAll(IList<Author> authors)
    {
        if (ValidationResult.IsValid)
        {
            ValidationResult.StatusCode = (int)StatusCodes.Ok;
        }
        return ValidationResult;
    }

    public MContentValidationResult ValidateAddAuthor(AuthorDto authorDto, Author authorWithSimilarId, Author authorWithSimilarName)
    {
        ValidationResult.Errors = new List<string>
        {
            _rules.ValidateGuidIfDefault(authorDto.Id),
            _rules.ValidateIfNullOrEmpty(authorDto.Name),
            _rules.ValidateIfNullOrEmpty(authorDto.Id.ToString()),
            _rules.ValidateUniqueAuthorId(authorWithSimilarId),
            _rules.ValidateUniqueAuthorName(authorWithSimilarName)
        }.Where(e => !string.IsNullOrEmpty(e)).ToList();
        if (ValidationResult.IsValid)
        {
            ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
        }
        return ValidationResult;
    }
}
}
