using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System.Linq.Expressions;

namespace E_Commerce.Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> 
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #endregion

        #region Include Expression

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            => IncludeExpressions.Add(includeExpression);

        #endregion

        #region Sorting

        public Expression<Func<TEntity,object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity,object>>? OrderByDescending { get; private set; }

        protected void SetOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void SetOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
            => OrderByDescending = orderByDescExpression;

        #endregion

        #region Pagination

        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; } = false;

        protected void ApplyPagination(int pageIndex, int pageSize) 
        {
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
            IsPaginated = true;
        }

        #endregion

    }
}
