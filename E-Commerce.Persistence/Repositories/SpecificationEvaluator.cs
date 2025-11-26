using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery, 
                      ISpecification<TEntity> specifications) where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;

            if(specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            query = specifications.Includes.Aggregate(query, (query, include) => query.Include(include));

            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if(specifications.OrderByDesc is not null)
                query = query.OrderByDescending(specifications.OrderByDesc);

            if (specifications.isPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            return query;
        }
    }
}
