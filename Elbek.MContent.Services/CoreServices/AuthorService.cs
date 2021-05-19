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
public interface IAuthorService
{
    Task<MContentResult<IList<AuthorDto>>> GetAuthorsAsync();
    Task<MContentResult<AuthorDto>> GetAuthorByIdAsync(Guid id);
    Task<MContentResult<AuthorDto>> AddAuthorAsync(AuthorDto authorDto);
    Task<MContentResult<AuthorDto>> UpdateAuthorAsync(Guid id, AuthorDto authorDto);
    Task<AuthorDto> DeleteAuthorAsync(AuthorDto authorDto);

}
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAuthorValidationService _validationService;
    public MContentResult<AuthorDto> ResultDto {
        get;
        set;
    } = new MContentResult<AuthorDto>();
    public MContentResult<IList<AuthorDto>> ResultListDto {
        get;
        set;
    } = new MContentResult<IList<AuthorDto>>();
    public AuthorService(IAuthorRepository repository, IMapper mapper, IAuthorValidationService validationService)
    {
        _repository = repository;
        _mapper = mapper;
        _validationService = validationService;
    }
    /// TODO 5 общее замечание по названию методов
    /// это AuthorService и в нем есть метод AddAuthorAsync, Author в названии метода уже не нужен
    /// нужно переименовать в AddAsync или CreateAsync
    /// и так везде
    public async Task<MContentResult<AuthorDto>> AddAuthorAsync(AuthorDto authorDto)
    {
        //validate
        /// TODO 4 authorWithSimilarId и authorWithSimilarName не испотльзуются в этом методе
        /// эти две переменные используются только в валидации
        /// нужно перенести вызовы репозиториев в валидационный сервис
        /// тоже самое в методе на Update
        var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
        var authorWithSimilarName = await _repository.GetAuthorByName(authorDto.Name);

        var validationResult = _validationService.ValidateAddAuthor(authorDto, authorWithSimilarId, authorWithSimilarName);
        if (!validationResult.IsValid)
        {
            return validationResult.ConvertFromValidationResult<AuthorDto>();
        }
        //map to domain object
        var author = _mapper.Map<Author>(authorDto);

        /// TODO 3 зачем тут try catch ?
        /// удалить
        /// тоже самое в методе на Update
        try
        {
            await _repository.AddAsync(author);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
        ResultDto.Data = authorDto;
        ResultDto.StatusCode = (int)StatusCodes.Created;
        return ResultDto;
    }

    //удалить
    public Task<AuthorDto> DeleteAuthorAsync(AuthorDto authorDto)
    {
        throw new NotImplementedException();
    }

    public async Task<MContentResult<AuthorDto>> GetAuthorByIdAsync(Guid id)
    {
        var author = await _repository.GetByIdAsync(id);

        //Validate
        var validationResult = _validationService.ValidateGetById(id, author);
        if (!validationResult.IsValid)
        {
            return validationResult.ConvertFromValidationResult<AuthorDto>();
        }
        var authorDto = _mapper.Map<AuthorDto>(author);
        ResultDto.Data = authorDto;
        ResultDto.StatusCode = (int)StatusCodes.Ok;
        return ResultDto;
    }

    public async Task<MContentResult<IList<AuthorDto>>> GetAuthorsAsync()
    {
        var authors = await _repository.GetAllAsync();
        var validationResult = _validationService.ValidateGetAll(authors);

        if (!validationResult.IsValid)
        {
            return validationResult.ConvertFromValidationResult<IList<AuthorDto>>();
        }
        var authorsDtos = _mapper.Map<IList<AuthorDto>>(authors);
        ResultListDto.Data = authorsDtos;
        ResultListDto.StatusCode = (int)StatusCodes.Ok;
        return ResultListDto;
    }

    public async Task<MContentResult<AuthorDto>> UpdateAuthorAsync(Guid id, AuthorDto authorDto)
    {
        var authorWithSimilarId = await _repository.GetByIdAsync(authorDto.Id);
        var authorWithSimilarName = await _repository.GetAuthorByName(authorDto.Name);

        var validationResult = _validationService.ValidateUpdateAuthor(id, authorDto, authorWithSimilarId, authorWithSimilarName);
        if (!validationResult.IsValid)
        {
            return validationResult.ConvertFromValidationResult<AuthorDto>();
        }
        //map to domain object
        var author = _mapper.Map<Author>(authorDto);
        try
        {
            await _repository.UpdateAsync(author);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
        ResultDto.Data = authorDto;
        ResultDto.StatusCode = (int)StatusCodes.Created;
        return ResultDto;
    }
}
}
