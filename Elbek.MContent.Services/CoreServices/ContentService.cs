using AutoMapper;
using Elbek.MContent.DataAccess.Repositories;
using Elbek.MContent.Services.Extensions;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.ValidationServices.ContentValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<MContentResult<ContentDto>> AddAsync(ContentDto TModel)
        {
            throw new NotImplementedException();
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

        public Task<MContentResult<ContentDto>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MContentResult<IList<ContentDto>>> GetByTypeAsync(int type)
        {
            throw new NotImplementedException();
        }
    }
}
