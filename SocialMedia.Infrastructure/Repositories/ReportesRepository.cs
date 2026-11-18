using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

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
                    Reportes.autoComentaristasMySql, new { Limit = limit }); // clases anonimas
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
        public async Task<IEnumerable<RankingPostUsuario>> GetRankingPostsUsuarioAsync(int limit = 3)
                {
                    try
                    {
                        return await _dapper.QueryAsync<RankingPostUsuario>(
                            Reportes.rankingPostsUsuarioMySql,
                            new { Limit = limit });
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
    }
}
