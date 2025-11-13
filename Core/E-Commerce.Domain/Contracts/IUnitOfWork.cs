using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        // Generic Repository
        IGenericRepository<TEntity , TKey> GetReository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;

        // Commit Changes
        Task<int> SaveChangesAsync();
    }
}
