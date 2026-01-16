using E_Commerce.Domain.Contracts;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>>? criteria) 
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; } = new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool isPaginated { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
            => Includes.Add(include);
        protected void AddOrderBy(Expression<Func<TEntity, object>> order)
            => OrderBy = order;
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> order)
            => OrderByDesc = order;
        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            isPaginated = true;
            Take = pageSize;
            Skip = pageSize * (pageIndex - 1);
        }
    }
}
