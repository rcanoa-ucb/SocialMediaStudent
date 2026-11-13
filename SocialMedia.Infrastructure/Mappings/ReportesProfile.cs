using AutoMapper;
using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.DTOs.Reporteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class ReportesProfile : Profile
    {
        public ReportesProfile()
        {
            CreateMap<AutoComentarista, AutoComentaristaDto>().ReverseMap();
        }
    }

}
