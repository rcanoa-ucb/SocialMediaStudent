using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;
using System;
using System.Collections.Generic;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly IDapperContext _dapper;

        public CommentRepository(SocialMediaContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10)
        {
            try
            {
                return await _dapper.QueryAsync<Comment>
                    (CommentQueries.comentariosMySql, new { Limit = limit });
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}