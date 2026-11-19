using System.Globalization;
using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
namespace SocialMedia.Services.Mappings;
public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(d => d.Date, x => x.MapFrom(s => s.Date.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)))
            .ForMember(d => d.Image, x => x.MapFrom(s => s.Imagen));
        CreateMap<PostDto, Post>()
            .ForMember(d => d.Date, x => x.MapFrom(s => DateTime.Parse(s.Date, CultureInfo.InvariantCulture)))
            .ForMember(d => d.Imagen, x => x.MapFrom(s => s.Image))
            .ForMember(d => d.User, x => x.Ignore())
            .ForMember(d => d.Comments, x => x.Ignore());
    }
}
