using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>();

            // El Id nunca se copia desde el DTO: lo genera la BD (insert) o ya existe (update)
            CreateMap<PostDto, Post>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore())
                .ForMember(d => d.Comments, o => o.Ignore());
        }
    }
}
