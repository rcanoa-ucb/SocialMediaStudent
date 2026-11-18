using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class RankingUsuarioRepository : IRankingUsuario
    {
        private readonly IDapperContext _dapper;
        public RankingUsuarioRepository(IDapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<RankingUsuario>> GetRankingUsuarioAsync(int user = 1)
        {
            try
            {
                return await _dapper.QueryAsync<RankingUsuario>
                    (Reportes.rankingUsuariosMySql, new { User = user });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
