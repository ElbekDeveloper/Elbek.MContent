using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.Utils;
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
        MContentValidationResult ValidateGetByType(string type);
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
        public async Task<IEnumerable<Author>> GetCorrespondingAuthorsAsync(ICollection<AuthorDto> authorDtos)
        {
            var result = new List<Author>();
            foreach (var authorDto in authorDtos)
            {
                if (authorDto.Id != Guid.Empty)
                {
                     result.Add(await _authorRepository.GetByIdAsync(authorDto.Id));
                }
                 result.Add(await _authorRepository.GetByName(authorDto.Name));
            }
            return result;
        }
        public  async Task<MContentValidationResult> ValidateAdd(ContentDto contentDto)
        {
            //Retrive data for validation
            var contentWithSimilarId = await _contentRepository.GetByIdAsync(contentDto.Id);//тут можно в запросе для валидации использовать Any(), чтобы не возврашать сущность, она все равно тут не нужна 
            Content contentWithSimilarTitle = null;
            var isParsable = EnumUtils.TryParseWithMemberName<ContentType>(contentDto.Type, out _);
            if (isParsable)
            {
                var parsedType = EnumUtils.GetEnumValueOrDefault<ContentType>(contentDto.Type);
               contentWithSimilarTitle  = await _contentRepository.GetByTitleAndType(contentDto.Title, parsedType);
            }
           // смотри выше
            var correnspondingAuthorsInDb = await GetCorrespondingAuthorsAsync(contentDto.Authors);

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
                _authorRules.ValidateMatchingAuthors(correnspondingAuthorsInDb, contentDto.Authors),
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

        public MContentValidationResult ValidateGetByType(string type)
        {
            ValidationResult.Errors = new List<string>
            {
                _contentRules.ValidateTypeRange(type),
                _contentRules.ValidateIfNullOrEmpty(type)
            }.Where(e => !string.IsNullOrEmpty(e)).ToList(); ;

            if (ValidationResult.Errors.Any())
            {
                ValidationResult.StatusCode = (int)StatusCodes.BadRequest;
            }
            return ValidationResult;
        }
    }
}
