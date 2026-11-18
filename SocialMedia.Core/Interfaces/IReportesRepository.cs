using SocialMedia.Core.CustomEntities.Reporteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IReportesRepository
    {
        Task<IEnumerable<AutoComentarista>>
            GetAutoComentaristasAsync(int limit = 10);

        Task<IEnumerable<RankingPostUsuario>>
            GetRankingPostsUsuarioAsync(int limit = 3);
    }
}
