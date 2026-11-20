
using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class ReportesService : IReportesService
    {
        public readonly IUnitOfWork _unitOfWork;

        public ReportesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RankingPostUsuario>>
            GetRankingPostsPorUsuarioAsync()
        {
            var ranking = await _unitOfWork.ReportesRepository
                .GetRankingPostsPorUsuarioAsync();

            return ranking;
        }
    }
}
