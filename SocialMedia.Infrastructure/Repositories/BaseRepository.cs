using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
namespace SocialMedia.Infrastructure.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly SocialMediaContext Context;
    protected readonly DbSet<T> Entities;
    public BaseRepository(SocialMediaContext context) { Context = context; Entities = context.Set<T>(); }
    public async Task<IEnumerable<T>> GetAllAsync() => await Entities.AsNoTracking().ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await Entities.FindAsync(id);
    public async Task AddAsync(T entity) => await Entities.AddAsync(entity);
    public void Update(T entity) => Entities.Update(entity);
    public void Delete(T entity) => Entities.Remove(entity);
}
