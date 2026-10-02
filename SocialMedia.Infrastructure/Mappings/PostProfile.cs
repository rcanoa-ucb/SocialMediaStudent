using AutoMapper;
using SocialMedia.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostProfile, PostDto >(); //.ReverseMap();
            CreateMap<PostDto, PostProfile>();
        }
    }
}
