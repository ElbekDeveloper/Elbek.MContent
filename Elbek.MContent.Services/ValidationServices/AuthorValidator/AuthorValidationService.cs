using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.ValidationServices.AuthorValidator
{
    public interface IAuthorValidationService
    {
        MContentValidationResult Validate(AuthorDto authorDto);
        MContentValidationResult Validate(Guid id);
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

        public MContentValidationResult Validate(Guid id)
        {
            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateIfNullOrEmpty(id.ToString()),
                _rules.ValidateGuidIfDefault(id)
            }.Where(e => string.IsNullOrEmpty(e) == false).ToList();

        
            return ValidationResult;
        }

        public MContentValidationResult Validate(AuthorDto authorDto)
        {
            ValidationResult.Errors = new List<string>
            {
                _rules.ValidateIfNullOrEmpty(authorDto.Id.ToString()),
                _rules.ValidateGuidIfDefault(authorDto.Id),
                _rules.ValidateIfNullOrEmpty(authorDto.Name)
            }.Where(e => string.IsNullOrEmpty(e) == false).ToList();

            return ValidationResult;
        }
    }
}
