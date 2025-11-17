using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity , TKey>(IQueryable<TEntity> inputQuery , 
            ISpecifications<TEntity , TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _dpcontext.Set<TEntity>()

            if (specifications.Criteria is not null)
                 query = query.Where(specifications.Criteria);
                 // query = _dpcontext.Set<Product>().Where(p => p.Id == 5);

            if (specifications.IncludeExpressions?.Count > 0)
            {
                query = specifications.IncludeExpressions.Aggregate(
                    query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            }

            // Sorting
            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            // query = query.Where(p => p.Id == 5).Include(p => p.ProductBrand).Include(p => p.ProductType);

            // Pagination
            if(specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            return query;
        }
    }
}
