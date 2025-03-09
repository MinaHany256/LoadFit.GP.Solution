using LoadFit.Core.Entities;
using LoadFit.Core.Repositories.Contract;
using LoadFit.Core.Specifications;
using LoadFit.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Vehicle))
            {
                return (IReadOnlyList<T>)await _dbContext.Set<Vehicle>().Include(P => P.Brand).Include(P => P.Type).ToListAsync();
            }

            return await _dbContext.Set<T>().ToListAsync();
        }

       
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Vehicle))
            {
                return await _dbContext.Set<Vehicle>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync() as T;
            }
            return await _dbContext.Set<T>().FindAsync(id);
        }



        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifications (ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
