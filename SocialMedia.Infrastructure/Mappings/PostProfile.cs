using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile() 
        {
            CreateMap<Post, PostDto>();//.ReverseMap(); sec puede hacer pero no se hace para mas claridad
            CreateMap<PostDto, Post>();

        }
    }
}
