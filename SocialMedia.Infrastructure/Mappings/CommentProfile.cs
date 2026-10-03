using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialMedia.Infrastructure.Mappings
{
    public class CommentProfile : Profile//hereda de una clase profile
    {
        public CommentProfile()
        { // PAQUETO AUTO MAPER CREATE MAP
            CreateMap<Comment, CommentDto>(); //conversion automatica de post a posDto // REVERSE MAP()
            CreateMap<CommentDto, Post>(); //conversion automatica de posDto a post
        }
    }
}