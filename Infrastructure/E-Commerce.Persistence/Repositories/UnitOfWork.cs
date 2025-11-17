using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DBContexts;
using System.Collections.Concurrent;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Field

        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositores;

        #endregion

        #region Constructor

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositores = new ();
        }

        #endregion

        #region Methods

        // Generic Repository
        public IGenericRepository<TEntity, TKey> GetReository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            => (IGenericRepository<TEntity, TKey>)_repositores.GetOrAdd(typeof(TEntity).Name, 
                (_) => new GenericRepository<TEntity, TKey>(_dbContext));
        

        // Save Changes
        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();

        #endregion

    }
}
