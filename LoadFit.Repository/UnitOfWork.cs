using LoadFit.Core;
using LoadFit.Core.Entities;
using LoadFit.Core.Order_Aggregate;
using LoadFit.Core.Repositories.Contract;
using LoadFit.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        // private Dictionary<string, GenericRepository<BaseEntity>> _repositories;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }


        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(Key, repository);
            }

            return _repositories[Key] as IGenericRepository<TEntity>;

        }
    }
}
