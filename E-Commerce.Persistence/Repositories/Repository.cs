using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories
{
    public class Repository<TEntity, TKey>(StoreDbContext context) 
        : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public void Add(TEntity entity)
            => context.Set<TEntity>().Add(entity);
                
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
            => await _dbSet.ApplySpecification(specification).ToListAsync();

        public async Task<TEntity>? GetAsync(ISpecification<TEntity> specification)
            => await _dbSet.ApplySpecification(specification).FirstOrDefaultAsync();  

        public async Task<TEntity>? GetByIdAsync(TKey id)
            => await context.Set<TEntity>().FindAsync(id);

        public async Task<int> GetCountAsync(ISpecification<TEntity> specification)
            => await _dbSet.ApplySpecification(specification).CountAsync();

        public void Remove(TEntity entity)
            => context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => context.Set<TEntity>().Update(entity);
    }
}
