using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Extensions;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.ValidationServices.AuthorValidator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.CoreServices
{
    public interface IAuthorService
    {
        Task<MContentResult<IList<AuthorDto>>> GetAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(Guid id);
        Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto);
        Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto);
        Task<AuthorDto> DeleteAuthorAsync(AuthorDto authorDto);

    }
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthorValidationService _validationService;
        public MContentResult<AuthorDto> ResultDto { get; set; } = new MContentResult<AuthorDto>();
        public MContentResult<IList<AuthorDto>> ResultListDto { get; set; } = new MContentResult<IList<AuthorDto>>();
        public AuthorService(IAuthorRepository repository, IMapper mapper, IAuthorValidationService validationService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto)
        {
            //Validate
            var validationResult = _validationService.Validate(authorDto);
            if (validationResult.IsValid == false)
            {
                /// TODO 2.8 ValidationException нам не нужны.
                /// все что в папке Filters можно удалить.
                /// Валидационный сервис должен валидировать обьект, возвращать строки ошибок(если они есть и код (400, 404)), то есть MContentValidationResult
                /// Если есть валидационные ошибки, то в методу автор сервиса ты должен иметь возможность вернуть юзеру все ошибки, то есть преобразовать MContentValidationResult в MContentResult
                /// Если валидационных ошибок нет, то дальше автор сервис делает свою работу и возвращает результат
            }

            /// TODO 2.5 Check for uniqueness of id и Check for uniqueness of name это валидация
            /// валидация должна быть в валидационном сервисе
            /// перенести все что касается валидации в валидационный сервис
            /// это касается всех методов этого класса
            
            //Check for uniqueness of id
            var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
            if (authorWithSimilarId != null)
            {
                /// TODO 2.7 не нужно делать throw new Exception

                throw new ArgumentException($"Author with Id '{authorDto.Id}' already exists");
            }
            //Check for uniqueness of name
            var authorWithSimilarName = await _repository.GetAuthorByName(authorDto.Name);
            if (authorWithSimilarName != null)
            {
                throw new ArgumentException($"Author with name '{authorDto.Name}' already exists");
            }
            //Map to domain object
            var author = _mapper.Map<Author>(authorDto);

            await _repository.AddAsync(author);
            return authorDto;
        }

        public Task<AuthorDto> DeleteAuthorAsync(AuthorDto authorDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(Guid id)
        {
            var author = await _repository.GetByIdAsync(id);

            //Validate
            var validationResult = _validationService.ValidateGetById(id, author);
            if (validationResult.IsValid == false)
            {
               // throw new ValidationException(validationResult.Errors, validationResult.StatusCode);
            }

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<MContentResult<IList<AuthorDto>>> GetAuthorsAsync()
        {
            var authors = await _repository.GetAllAsync();
            var validationResult = _validationService.ValidateGetAll(authors);

            if (!validationResult.IsValid)
            {
                return ResultExtensions.ConvertFromValidationResult<IList<AuthorDto>>(validationResult);
            }
            var authorsDtos = _mapper.Map<IList<AuthorDto>>(authors);
            ResultListDto.Data = authorsDtos;
            ResultListDto.StatusCode = (int)StatusCodes.Ok;
            return ResultListDto;
        }

        public async Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto)
        {
            //Validate
            var validationResult = _validationService.Validate(authorDto);
            if (validationResult.IsValid == false)
            {
            }
            //Check for similarity of id
            var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
            if (authorWithSimilarId == null)
            {
                throw new ArgumentException($"Author with Id '{authorDto.Id}' was not found");
            }
            //Check for uniqueness of name
            var authorWithSimilarName = await _repository.GetAuthorByName(authorDto.Name);
            if (authorWithSimilarName != null)
            {
                throw new ArgumentException($"Author with Id '{authorWithSimilarName.Id}' already has Name '{authorDto.Name}'");
            }
            //Map to domain object
            var author = _mapper.Map<Author>(authorDto);
            try
            {
                await _repository.UpdateAsync(author);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return authorDto;
        }
    }
}
