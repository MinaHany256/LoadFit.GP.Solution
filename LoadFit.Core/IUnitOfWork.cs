using LoadFit.Core.Entities;
using LoadFit.Core.Order_Aggregate;
using LoadFit.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
       
        Task<int> CompleteAsync();




    }
}
