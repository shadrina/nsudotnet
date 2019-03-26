using System.Collections.Generic;

namespace AlgoMe.Models.Repository {
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
        void DeleteAll(ICollection<TEntity> entities);
    }
}