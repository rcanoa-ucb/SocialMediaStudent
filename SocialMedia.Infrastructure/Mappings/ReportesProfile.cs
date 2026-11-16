using AutoMapper;
using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.DTOs.Reporteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class ReportesProfileProfile : Profile
    {
        public ReportesProfileProfile()
        {
            CreateMap<AutoComentarista, AutoComentaristaDto>().ReverseMap();
            CreateMap<RankingPostUsuario, RankingPostUsuarioDto>().ReverseMap();
        }
    }

}
