using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories
{
    public class Repository<TEntity, TKey>(StoreDbContext context) 
        : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        public void Add(TEntity entity)
            => context.Set<TEntity>().Add(entity);
                
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity>? GetByIdAsync(TKey id)
            => await context.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
            => context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => context.Set<TEntity>().Update(entity);
    }
}
