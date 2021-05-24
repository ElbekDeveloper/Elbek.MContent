using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.ValidationServices.AuthorValidator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly IContentRepository _contentRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IContentValidationRules _contentRules;
        private readonly IAuthorValidationRules _authorRules;

        public MContentValidationResult ValidationResult { get; private set; } = new MContentValidationResult();

        public ContentValidationService(IContentRepository repository, IContentValidationRules rules, IAuthorValidationRules authorRules, IAuthorRepository authorRepository)
        {
            _contentRepository = repository;
            _contentRules = rules;
            _authorRules = authorRules;
            _authorRepository = authorRepository;
        }
        public async Task<ICollection<AuthorDto>> GetUnmatchedAuthorsAsync(ICollection<AuthorDto> authorDtos)
        {
            throw new NotImplementedException();
            var unmatchingAuthorsInDb = new Collection<AuthorDto>();
            //Validate authors existence in db
            foreach (var authorDto in authorDtos)
            {
                var hasDefaultId = _authorRules.ValidateGuidIfDefault(authorDto.Id);
                if (!string.IsNullOrEmpty(hasDefaultId))
                {

                }
            }
            return unmatchingAuthorsInDb;
        }
        public  async Task<MContentValidationResult> ValidateAdd(ContentDto contentDto)
        {
            //Retrive data for validation
            var contentWithSimilarId = await _contentRepository.GetByIdAsync(contentDto.Id);//тут можно в запросе для валидации использовать Any(), чтобы не возврашать сущность, она все равно тут не нужна 
            var contentWithSimilarTitle= await _contentRepository.GetByTitle(contentDto.Title, contentDto.Type); // смотри выше
            var unmatchedAuthorsInDb = await GetUnmatchedAuthorsAsync(contentDto.Authors);

            ValidationResult.Errors = new List<string>
            {
                _contentRules.ValidateGuidIfDefault(contentDto.Id),
                _contentRules.ValidateIfNullOrEmpty(contentDto.Id.ToString()),
                _contentRules.ValidateUniqueContentId(contentWithSimilarId),
                _contentRules.ValidateIfNullOrEmpty(contentDto.Title),
                _contentRules.ValidateUniqueTitleOfItsType(contentWithSimilarTitle),
                _contentRules.ValidateIfNullOrEmpty(contentDto.Type.ToString()),
                _contentRules.ValidateTypeRange(contentDto.Type),
                _authorRules.ValidateIfAnyAuthorExists(contentDto.Authors),
                _authorRules.ValidateUnmatchedAuthors(unmatchedAuthorsInDb),
                _authorRules.ValidateAgainstDuplicates(contentDto.Authors)
            }.Where(e => !string.IsNullOrEmpty(e)).ToList();

            if (!ValidationResult.IsValid)
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }

        public async Task<MContentValidationResult> ValidateGetAll()
        {
            var data = await _contentRepository.GetAllAsync();


            ValidationResult.StatusCode = (int)StatusCodes.Ok;


            return ValidationResult;
        }

        public async Task<MContentValidationResult> ValidateGetById(Guid id)
        {
            var content = await _contentRepository.GetByIdAsync(id);
            string idIsEmpty = _contentRules.ValidateIfNullOrEmpty(id.ToString());
            string idIsDefault = _contentRules.ValidateGuidIfDefault(id);
            string contentWasFound = _contentRules.ValidateContentWasFound(id, content);

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
                _contentRules.ValidateTypeRange(type),
                _contentRules.ValidateIfNullOrEmpty(type.ToString())
            }.Where(e => !string.IsNullOrEmpty(e)).ToList(); ;

            if (ValidationResult.Errors.Any())
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }
    }
}
