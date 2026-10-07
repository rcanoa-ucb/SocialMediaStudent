using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class PostProfile : Profile//hereda de una clase profile
    {
        public PostProfile()
        { // PAQUETO AUTO MAPER CREATE MAP
            CreateMap<Post, PostDto>(); //conversion automatica de post a posDto // REVERSE MAP()
            CreateMap<PostDto, Post>(); //conversion automatica de posDto a post
        }
    }
}
