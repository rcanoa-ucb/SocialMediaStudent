using AutoMapper;
using SocialMedia.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentProfile, CommentDto>(); //.ReverseMap();
            CreateMap<CommentDto, CommentProfile>();
        }
    }
}
