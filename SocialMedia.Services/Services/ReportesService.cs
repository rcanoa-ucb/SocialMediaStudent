using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AutoComentarista>>
            GetAutoComentaristasAsync(int limit = 10)
        {
            return await _unitOfWork.ReportesRepository
                .GetAutoComentaristasAsync(limit);
        }

        public async Task<IEnumerable<RankingPostUsuario>>
            GetRankingPostsUsuarioAsync(int limit = 3)
        {
            return await _unitOfWork.ReportesRepository.GetRankingPostsUsuarioAsync(limit);
        }
    }
}