using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
    .ForMember(dest => dest.Date,
        opt => opt.ConvertUsing<DateTimeToStringConverter, DateTime>())
    .ForMember(dest => dest.IsActive,
        opt => opt.MapFrom(src => src.IsActive == 1UL));

            CreateMap<CommentDto, Comment>()
                .ForMember(dest => dest.Date,
                    opt => opt.ConvertUsing<StringToDateTimeConverter, string>())
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.IsActive ? 1UL : 0UL));
        }
    }
}
