using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        public readonly IPostRepository? _postRespository;
        public readonly IBaseRepository<User>? _userRespository;
        public readonly IBaseRepository<Comment>? _commentRespository;
        public readonly IReportesRepository? _reportesRespository;

        //Dapper
        private readonly IDapperContext _dapper;
        private IDbContextTransaction? _efTransaction;

        public UnitOfWork(SocialMediaContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public IPostRepository PostRepository =>
            _postRespository ?? new PostRepository(_context, _dapper);

        public IBaseRepository<User> UserRespository =>
             _userRespository ?? new BaseRepository<User>(_context);

        public IBaseRepository<Comment> CommentRepository =>
            _commentRespository ?? new BaseRepository<Comment>(_context);

        public IReportesRepository ReportesRepository =>
            _reportesRespository ?? new ReportesRepository(_dapper);

        public void Dispose()
        {
            if (_context != null)
            {
                _efTransaction?.Dispose();
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Dapper
        public async Task BeginTransactionAsync()
        {
            if (_efTransaction == null)
            {
                _efTransaction = await _context.Database.BeginTransactionAsync();

                // registrar la conexión/tx en DapperContext
                var conn = _context.Database.GetDbConnection();
                var tx = _efTransaction.GetDbTransaction();
                _dapper.SetAmbientConnection(conn, tx);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_efTransaction != null)
                {
                    await _efTransaction.CommitAsync();
                    _efTransaction.Dispose();
                    _efTransaction = null;
                }
            }
            finally
            {
                _dapper.ClearAmbientConnection();
            }
        }

        public async Task RollbackAsync()
        {
            if (_efTransaction != null)
            {
                await _efTransaction.RollbackAsync();
                _efTransaction.Dispose();
                _efTransaction = null;
            }
            _dapper.ClearAmbientConnection();
        }

        public IDbConnection? GetDbConnection()
        {
            // Retornamos la conexión subyacente del DbContext
            return _context.Database.GetDbConnection();
        }

        public IDbTransaction? GetDbTransaction()
        {
            return _efTransaction?.GetDbTransaction();
        }

        #endregion
    }
}
