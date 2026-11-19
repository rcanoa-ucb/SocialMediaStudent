using SocialMedia.Core.CustomEntities.Reporteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Services.Interfaces
{
    public interface IReportesService
    {
        Task<IEnumerable<AutoComentarista>> GetAutoComentaristasAsync(int limit = 10);
        Task<IEnumerable<RankingMejorPostPorUsuario>> GetRankingMejorPostPorUsuarioAsync();
        Task<IEnumerable<RankingMejorPostPorUsuario>> GetMejorPostPorUsuarioAsync() => GetRankingMejorPostPorUsuarioAsync();
    }
}
