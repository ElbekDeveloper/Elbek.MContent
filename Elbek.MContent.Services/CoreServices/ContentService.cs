using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Extensions;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.ValidationServices.ContentValidator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.CoreServices
{
    public interface IContentService : ICoreService<ContentDto>
    {
        Task<MContentResult<IList<ContentDto>>> GetByTypeAsync(int type);

    }
    public class ContentService : IContentService
    {
        private readonly IContentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IContentValidationService _validationService;
        public MContentResult<ContentDto> ResultDto { get; set; } = new MContentResult<ContentDto>();
        public MContentResult<IList<ContentDto>> ResultListDto { get; set; } = new MContentResult<IList<ContentDto>>();

        public ContentService(IContentRepository repository, IMapper mapper, IContentValidationService validationService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<MContentResult<ContentDto>> AddAsync(ContentDto contentDto)
        {
            //Validate
            var validationResult = await _validationService.ValidateAdd(contentDto);
            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<ContentDto>();
            }
            //Map to domain object
            var entityForDb = _mapper.Map<Content>(contentDto);
            var addedEntity = await _repository.AddAsync(entityForDb);
            var retrievedEntity = await _repository.GetByIdAsync(addedEntity.Id);
            //Map to dto
            var resultEntity = _mapper.Map<ContentDto>(retrievedEntity);
            ResultDto.Data = resultEntity;
            ResultDto.StatusCode = (int)StatusCodes.Created;
            return ResultDto;
        }

        public async Task<MContentResult<IList<ContentDto>>> GetAllAsync()
        {
            var validationResult = await _validationService.ValidateGetAll();

            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<IList<ContentDto>>();
            }

            var contents = await _repository.GetAllAsync();
            var contentDtos = _mapper.Map<IList<ContentDto>>(contents);

            ResultListDto.Data = contentDtos;
            ResultListDto.StatusCode = (int)StatusCodes.Ok;
            return ResultListDto;
        }

        public Task<MContentResult<ContentDto>> UpdateAsync(Guid id, ContentDto TModel)
        {
            throw new NotImplementedException();
        }

        public async Task<MContentResult<ContentDto>> GetByIdAsync(Guid id)
        {
            var validationResult = await _validationService.ValidateGetById(id);
            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<ContentDto>();
            }

            //Retrieve
            var content = await _repository.GetByIdAsync(id);
            var contentDto = _mapper.Map<ContentDto>(content);
            //Return result
            ResultDto.Data = contentDto;
            ResultDto.StatusCode = (int)StatusCodes.Ok;
            return ResultDto;
        }

        public async Task<MContentResult<IList<ContentDto>>> GetByTypeAsync(int type)
        {
            var validationResult = _validationService.ValidateGetByType(type);
            if (!validationResult.IsValid)
            {
                return validationResult.ConvertFromValidationResult<IList<ContentDto>>();
            }
            var contents = await _repository.GetByTypeAsync(type);
            var contentDtos = _mapper.Map<IList<ContentDto>>(contents);

            ResultListDto.Data = contentDtos;
            ResultListDto.StatusCode = (int)StatusCodes.Ok;
            return ResultListDto;
        }
    }
}
