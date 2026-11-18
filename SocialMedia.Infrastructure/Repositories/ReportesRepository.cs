using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Queries;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ReportesRepository : IReportesRepository
    {
        private readonly IDapperContext _dapper;
        public ReportesRepository(IDapperContext dapper)
        {
            _dapper = dapper;
        }

        // --- FUNCIÓN QUE YA TENÍAS (NO TOCAR) ---
        public async Task<IEnumerable<AutoComentarista>> GetAutoComentaristasAsync(int limit = 10)
        {
            try
            {
                return await _dapper.QueryAsync<AutoComentarista>(
                    Reportes.autoComentaristasMySql, new { Limit = limit });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // --- NUEVA FUNCIÓN PARA LA TAREA ---
        public async Task<IEnumerable<RankingPost>> GetRankingPostsPorUsuarioAsync()
        {
            try
            {
                return await _dapper.QueryAsync<RankingPost>(Reportes.rankingPostsPorUsuarioMySql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}