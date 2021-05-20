using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator
{
    public interface IAuthorValidationService : IValidationService<AuthorDto>
    {

    }

    public class AuthorValidationService : IAuthorValidationService
    {
        private readonly IAuthorValidationRules _rules;
        private readonly IAuthorRepository _repository;
        public MContentValidationResult ValidationResult { get; private set; } = new MContentValidationResult();

        public AuthorValidationService(IAuthorValidationRules rules, IAuthorRepository repository)
        {
            _rules = rules;
            _repository = repository;
        }

        public async Task<MContentValidationResult> ValidateGetById(Guid id)
        {
            var author = await _repository.GetByIdAsync(id);
            string idIsEmpty = _rules.ValidateIfNullOrEmpty(id.ToString());
            string idIsDefault = _rules.ValidateGuidIfDefault(id);
            string authorWasFound = _rules.ValidateAuthorWasFound(id, author);

            ValidationResult.Errors = new List<string>
            {
                idIsEmpty,
                idIsDefault,
                authorWasFound
            }.Where(e => !string.IsNullOrEmpty(e)).ToList();

            if (string.IsNullOrEmpty(authorWasFound))
            {
                ValidationResult.StatusCode = (int)StatusCodes.NotFound;
            }
            else if (string.IsNullOrEmpty(idIsDefault) || string.IsNullOrEmpty(idIsEmpty))
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }

        public async Task<MContentValidationResult> ValidateUpdate(Guid id, AuthorDto authorDto)
        {
            var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
            var authorWithSimilarName = await _repository.GetAuthorByName(authorDto.Name);

            string checkIfAuthorWasFound = _rules.ValidateAuthorWasFound(id, authorWithSimilarId);
            
            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateIfIdsAreSame(id, authorDto.Id),
                _rules.ValidateUniqueAuthorName(authorWithSimilarName),
                checkIfAuthorWasFound,
                _rules.ValidateIfNullOrEmpty(authorDto.Id.ToString()),
                _rules.ValidateGuidIfDefault(authorDto.Id),
                _rules.ValidateIfNullOrEmpty(authorDto.Name)
            }.Where(e => !string.IsNullOrEmpty(e)).ToList();

            if (string.IsNullOrEmpty(checkIfAuthorWasFound))
            {
                ValidationResult.StatusCode = (int)StatusCodes.NotFound;
            }
            else if (ValidationResult.Errors.Any())
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }

        public async Task<MContentValidationResult> ValidateGetAll()
        {
            var data = await _repository.GetAllAsync();

            if (data.Count >= 0)
            {
                ValidationResult.StatusCode = (int)StatusCodes.Ok;
            }
            else
            {
                ValidationResult.Errors = new List<string> { "Unhandled exception was thrown during data retrieve " };
            }

            return ValidationResult;
        }

        public async Task<MContentValidationResult> ValidateAdd(AuthorDto authorDto)
        {
            //Retrive data for validation
            var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
            var authorWithSimilarName = await _repository.GetAuthorByName(authorDto.Name);
            
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
