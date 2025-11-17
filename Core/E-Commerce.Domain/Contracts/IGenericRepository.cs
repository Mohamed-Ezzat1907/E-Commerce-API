using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        // Get All Entities
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);

        // Get Entity By Id
        Task<TEntity?> GetByIdAsync(TKey id);

        // Add New Entity
        Task AddAsync(TEntity entity);

        // Update Existing Entity
        void Update(TEntity entity);

        // Delete Entity
        void Delete(TEntity entity);
    }
}
