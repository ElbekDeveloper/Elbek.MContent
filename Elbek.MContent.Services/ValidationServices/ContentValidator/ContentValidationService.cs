using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices.ContentValidator
{
    public interface IContentValidationService : IValidationService<ContentDto>
    {
        MContentValidationResult ValidateGetByType(int type);
    }
    public class ContentValidationService : IContentValidationService
    {
        private readonly IContentRepository _repository;
        private readonly IContentValidationRules _rules;
        public MContentValidationResult ValidationResult { get; private set; } = new MContentValidationResult();

        public ContentValidationService(IContentRepository repository, IContentValidationRules rules)
        {
            _repository = repository;
            _rules = rules;
        }

        public  async Task<MContentValidationResult> ValidateAdd(ContentDto contentDto)
        {
            //Retrive data for validation
            var contentWithSimilarId = await _repository.GetByIdAsync(contentDto.Id);
            var contentWithSimilarTitle= await _repository.GetContentByTitle(contentDto.Title, contentDto.Type);

            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateGuidIfDefault(contentDto.Id),
                _rules.ValidateIfNullOrEmpty(contentDto.Id.ToString()),
                _rules.ValidateUniqueContentId(contentWithSimilarId),
                _rules.ValidateIfNullOrEmpty(contentDto.Title),
                _rules.ValidateUniqueTitleOfItsType(contentWithSimilarTitle),
                _rules.ValidateIfNullOrEmpty(contentDto.Type.ToString()),
                _rules.ValidateTypeRange(contentDto.Type)
                //validation for contents should be added
            }.Where(e => !string.IsNullOrEmpty(e)).ToList();

            if (!ValidationResult.IsValid)
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

        public async Task<MContentValidationResult> ValidateGetById(Guid id)
        {
            var content = await _repository.GetByIdAsync(id);
            string idIsEmpty = _rules.ValidateIfNullOrEmpty(id.ToString());
            string idIsDefault = _rules.ValidateGuidIfDefault(id);
            string contentWasFound = _rules.ValidateContentWasFound(id, content);

            ValidationResult.Errors = new List<string>
            {
                idIsEmpty,
                idIsDefault,
                contentWasFound
            }.Where(e => !string.IsNullOrEmpty(e)).ToList();

            if (string.IsNullOrEmpty(contentWasFound))
            {
                ValidationResult.StatusCode = (int)StatusCodes.NotFound;
            }
            else if (string.IsNullOrEmpty(idIsDefault) || string.IsNullOrEmpty(idIsEmpty))
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }

        public Task<MContentValidationResult> ValidateUpdate(Guid id, ContentDto model)
        {
            throw new NotImplementedException();
        }

        public MContentValidationResult ValidateGetByType(int type)
        {
            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateTypeRange(type),
                _rules.ValidateIfNullOrEmpty(type.ToString())
            }.Where(e => !string.IsNullOrEmpty(e)).ToList(); ;

            if (ValidationResult.Errors.Any())
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }
    }
}
