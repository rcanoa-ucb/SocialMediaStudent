using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class CommentProfile : Profile//hereda de una clase profile
    {
        public CommentProfile()
        { // PAQUETO AUTO MAPER CREATE MAP
            CreateMap<Comment, CommentDto>(); //conversion automatica de comment a commentDto// REVERSE MAP()
            CreateMap<CommentDto, Comment>(); //conversion automatica de commentDto a comment
        }
    }
}
