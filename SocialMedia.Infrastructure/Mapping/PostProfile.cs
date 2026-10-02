using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mapping
{
    public class PostProfile : Profile //heredamos de una clase que se llama profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>(); //que realize la conversion automatica de pst a postDto
            CreateMap<PostDto, Post>(); // de PostDto a post tambien pordia ser CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
