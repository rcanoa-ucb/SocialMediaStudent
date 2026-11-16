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

        public async Task<IEnumerable<RankingPostUsuario>> GetRankingPostsPorUsuarioAsync(int top = 3)
        {
            try
            {
                if (top <= 0) top = 3;
                return await _dapper.QueryAsync<RankingPostUsuario>(
                    Reportes.rankingPostsPorUsuarioMySql, new { Top = top });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
