using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Content, ContentDto>()
                .ForMember(dto => dto.Authors, opt => opt.MapFrom(c => c.ContentAuthors.Select(ca => ca.Author)));
        }
    }
}
