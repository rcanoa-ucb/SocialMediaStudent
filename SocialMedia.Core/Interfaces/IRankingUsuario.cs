using SocialMedia.Core.CustomEntities.Reporteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IRankingUsuario
    {
        Task<IEnumerable<RankingUsuario>> GetRankingUsuarioAsync(int user = 1);
    }
}
