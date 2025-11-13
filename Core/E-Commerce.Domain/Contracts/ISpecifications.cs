using E_Commerce.Domain.Entities;
using System.Linq.Expressions;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity , bool>>? Criteria { get; } // p => p.Id

        public List<Expression<Func<TEntity , Object>>> IncludeExpressions { get; }
    }
}
