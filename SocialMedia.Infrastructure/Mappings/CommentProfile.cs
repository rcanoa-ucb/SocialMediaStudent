using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>();

            CreateMap<CommentDto, Comment>()
    .ForMember(d => d.Id, o => o.Ignore())
    .ForMember(d => d.Post, o => o.Ignore())
    .ForMember(d => d.User, o => o.Ignore());
        }
    }
}