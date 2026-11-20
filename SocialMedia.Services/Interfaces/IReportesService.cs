
using SocialMedia.Core.CustomEntities.Reporteria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Services.Interfaces
{
    public interface IReportesService
    {
        Task<IEnumerable<RankingPostUsuario>>
            GetRankingPostsPorUsuarioAsync();
    }
}
