using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity>? GetByIdAsync(TKey id);
        Task<TEntity>? GetAsync(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
        Task<int> GetCountAsync(ISpecification<TEntity> specification);
    }
}
