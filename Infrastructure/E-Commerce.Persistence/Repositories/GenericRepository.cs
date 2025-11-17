using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Fields

        private readonly StoreDbContext _dbContext; 

        #endregion

        #region Constructor

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        // Get All Entities
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
            => asNoTracking ? await _dbContext.Set<TEntity>().ToListAsync()
                        : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        // Get Entity By Id
        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        // Add New Entity
        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        // Update Existing Entity
        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        // Delete Entity
        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        #endregion
    }
}
