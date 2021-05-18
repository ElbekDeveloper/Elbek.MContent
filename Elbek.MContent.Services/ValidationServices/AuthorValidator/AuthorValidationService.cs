using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator
{
    public interface IAuthorValidationService
    {
        MContentValidationResult Validate(AuthorDto authorDto);
        MContentValidationResult ValidateGetById(Guid id, Author author);
        MContentValidationResult ValidateGetAll(IList<Author> authors);
    }

    public class AuthorValidationService : IAuthorValidationService
    {
        private readonly IAuthorValidationRules _rules;
        public MContentValidationResult ValidationResult { get; private set; } = new MContentValidationResult();

        public AuthorValidationService(IAuthorValidationRules rules)
        {
            _rules = rules;
        }


        /// TODO 2.6 сдесь у тебя должны быть валидационные методы на каждый метод сервиса, который он валидирует
        /// создать методы: ValidateCreate, ValidateUpdate, ValidateGetById

        public MContentValidationResult ValidateGetById(Guid id, Author author)
        {
            //
            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateIfNullOrEmpty(id.ToString()),
                _rules.ValidateGuidIfDefault(id),
                _rules.ValidateFoundAuthor(id, author)
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

        public MContentValidationResult Validate(AuthorDto authorDto)
        {
            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateIfNullOrEmpty(authorDto.Id.ToString()),
                _rules.ValidateGuidIfDefault(authorDto.Id),
                _rules.ValidateIfNullOrEmpty(authorDto.Name)
            }.Where(e => !string.IsNullOrEmpty(e)).ToList();

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
    }
}
