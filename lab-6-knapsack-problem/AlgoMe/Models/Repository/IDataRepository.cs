using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AlgoMe.Models.Repository {
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}