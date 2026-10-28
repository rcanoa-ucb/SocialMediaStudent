using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context; // conetxto a todas las tabals de la base de datos
        private readonly DbSet<T> _entities;// solo una tabla 
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        { 
            return await _entities.ToListAsync();  // ToListAsync devolver toda la lista de los tipos T (DATOS TIPO T)
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }
        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
