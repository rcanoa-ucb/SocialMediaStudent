using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Services.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AutoComentarista>> GetAutoComentaristasAsync(int limit = 10)
        {
            return await _unitOfWork.ReportesRepository.GetAutoComentaristasAsync(limit);
        }

        // NUEVO MÉTODO PARA LA TAREA 
        public async Task<IEnumerable<RankingPost>> GetRankingPostsPorUsuarioAsync()
        {
            return await _unitOfWork.ReportesRepository.GetRankingPostsPorUsuarioAsync();
        }
    }

}