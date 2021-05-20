using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Extensions;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.ValidationServices.AuthorValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.CoreServices
{
    public interface IAuthorService : ICoreService<AuthorDto>
    {

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
        public async Task<MContentResult<AuthorDto>> AddAsync(AuthorDto authorDto)
        {
            //Validate
            var validationResult = await _validationService.ValidateAdd(authorDto);
            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<AuthorDto>();
            }
            //Map to domain object
            var entityForDb = _mapper.Map<Author>(authorDto);
            var addedEntity =   await _repository.AddAsync(entityForDb);
            //Map to dto
            var resultEntity = _mapper.Map<AuthorDto>(addedEntity);
            ResultDto.Data = resultEntity;
            ResultDto.StatusCode = (int)StatusCodes.Created;
            return ResultDto;
        }

        public async Task<MContentResult<AuthorDto>> GetByIdAsync(Guid id)
        {

            //Validate
            var validationResult = await _validationService.ValidateGetById(id);
            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<AuthorDto>();
            }
            //Retrieve
            var author = await _repository.GetByIdAsync(id);
            var authorDto = _mapper.Map<AuthorDto>(author);
            //Return result
            ResultDto.Data = authorDto;
            ResultDto.StatusCode = (int)StatusCodes.Ok;
            return ResultDto;
        }

        public async Task<MContentResult<IList<AuthorDto>>> GetAllAsync()
        {
            var validationResult = await _validationService.ValidateGetAll();

            if (!validationResult.IsValid)
            {
               return validationResult.ConvertFromValidationResult<IList<AuthorDto>>();
            }

            var authors = await _repository.GetAllAsync();
            var authorsDtos = _mapper.Map<IList<AuthorDto>>(authors);
            ResultListDto.Data = authorsDtos;
            ResultListDto.StatusCode = (int)StatusCodes.Ok;
            return ResultListDto;
        }

        public async Task<MContentResult<AuthorDto>> UpdateAsync(Guid id, AuthorDto authorDto)
        {


            var validationResult = await _validationService.ValidateUpdate(id, authorDto);
            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<AuthorDto>();
            }
            //map to domain object
            var entityForDb = _mapper.Map<Author>(authorDto);
            
            var updatedEntity =  await _repository.UpdateAsync(entityForDb);
            //Map to dto
            var resultEntity = _mapper.Map<AuthorDto>(updatedEntity);

            ResultDto.Data = resultEntity;
            ResultDto.StatusCode = (int)StatusCodes.Created;
            return ResultDto;
        }
    }
}
