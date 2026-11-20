using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    internal class ReportesRepository : IReportesRepository
    {
        private IDapperContext dapper;

        public ReportesRepository(IDapperContext dapper)
        {
            this.dapper = dapper;
        }

        public Task<IEnumerable<RankingPostUsuario>> GetRankingPostsPorUsuarioAsync()
        {
            throw new NotImplementedException();
        }
    }
}