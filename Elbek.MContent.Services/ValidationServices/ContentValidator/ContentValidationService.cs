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

        public Task<MContentValidationResult> ValidateAdd(ContentDto model)
        {
            throw new NotImplementedException();
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

        public Task<MContentValidationResult> ValidateGetById(Guid id)
        {
            throw new NotImplementedException();
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
