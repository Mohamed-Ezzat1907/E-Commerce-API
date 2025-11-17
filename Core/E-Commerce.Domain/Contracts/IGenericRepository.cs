using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        // Get All Entities
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);

        // Get Entity By Id
        Task<TEntity?> GetByIdAsync(TKey id);

        #region Specifications

        // Get Entities By Specification
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity , TKey> specification);

        // Get Single Entity By Specification
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity , TKey> specification);

        #endregion

        #region Pagination

        Task<int> CountAsync(ISpecifications<TEntity,TKey> specifications);

        #endregion

        // Add New Entity
        Task AddAsync(TEntity entity);

        // Update Existing Entity
        void Update(TEntity entity);

        // Delete Entity
        void Delete(TEntity entity);
    }
}
