using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AlgoMe.Models.Repository {
    public interface IDataRepository<TEntity> : IDisposable
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(long id);
        Task<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity dbEntity, TEntity entity);
        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}