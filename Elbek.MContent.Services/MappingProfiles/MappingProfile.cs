using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;

namespace Elbek.MContent.Services.MappingProfiles
{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Content, ContentDto>().ReverseMap();
    }
}
}
