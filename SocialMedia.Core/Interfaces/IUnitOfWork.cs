using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IBaseRepository<User> UserRespository { get; }
        ICommentRepository CommentRepository { get; }
        IReportesRepository ReportesRepository { get; }
        IRankingUsuario RankingUsuarioRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();

        //Dapper
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

        IDbConnection? GetDbConnection();
        IDbTransaction? GetDbTransaction();
    }
}
