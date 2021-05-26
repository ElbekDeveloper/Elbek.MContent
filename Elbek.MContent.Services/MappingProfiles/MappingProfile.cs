using AutoMapper;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            //Domain object to DTO
            CreateMap<Content, ContentDto>()
                .ForMember(dto => dto.Authors, opt => opt.MapFrom(c => c.ContentAuthors.Select(ca => ca.Author)));
            //DTO to Domain object
            CreateMap<ContentDto, Content>()
                .ForMember
                (
                //Custom mapping logic starts here
                c => c.ContentAuthors,
                    opt => opt.MapFrom
                    (
                        dto => dto.Authors.Select
                        (
                            a => new ContentAuthors
                            {
                                Id = Guid.NewGuid(),
                                AuthorId = a.Id,
                                ContentId = dto.Id,
                            }
                        )
                    )
                ).ForMember(c => c.Type, opt => opt.MapFrom(dto => EnumUtils.GetEnumValueOrDefault<ContentType>(dto.Type)));
            //Custom mapping logic ends here

        }
    }
}
