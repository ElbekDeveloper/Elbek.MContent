using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Exceptions;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.ValidationServices.AuthorValidator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.CoreServices
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
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
                throw new ValidationException(validationResult.Errors, validationResult.StatusCode);
            }
            //Check for uniqueness of id
            var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
            if (authorWithSimilarId != null)
            {
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
            //Validate
            var validationResult = _validationService.Validate(id);
            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult.Errors, validationResult.StatusCode);
            }
            //Retrive 
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
               throw new VersionNotFoundException($"Author with '{id}' was not found");
            }
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
            var authors = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto)
        {
            //Validate
            var validationResult = _validationService.Validate(authorDto);
            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult.Errors, validationResult.StatusCode);
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
