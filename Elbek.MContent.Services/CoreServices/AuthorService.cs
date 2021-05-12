using AutoMapper;
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
    }
}
