using AutoMapper;
using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.DTOs.Reporteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class ReportesProfile: Profile
    {
        public ReportesProfile()
        {
            CreateMap<AutoComentarista, AutoComentaristaDto>().ReverseMap(); //ReverseMap de dro al otro lado o del otro lado a Dto 
            CreateMap<RankingPostUsuario, RankingPostUsuarioDto>().ReverseMap();
            //CreateMap<AutoComentarista, AutoComentaristaDto>();
            //CreateMap<AutoComentaristaDto, AutoComentarista>() // reversemao evita ahcer dos linea
        }
    }
}
