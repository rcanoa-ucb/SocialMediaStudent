
using SocialMedia.Core.CustomEntities.Reporteria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IReportesRepository
    {
        Task<IEnumerable<RankingPostUsuario>>
            GetRankingPostsPorUsuarioAsync();
    }
}
