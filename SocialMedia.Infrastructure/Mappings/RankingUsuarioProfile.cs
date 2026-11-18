using AutoMapper;
using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class RankingUsuarioProfile: Profile
    {
        public RankingUsuarioProfile()
        {
            CreateMap<RankingUsuario,RankigUsuarioDto>().ReverseMap();
        }
    }
}
