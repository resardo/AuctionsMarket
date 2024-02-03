using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace DAL.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        TEntity GetById(TKey id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TKey id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void PatchUpdate(TEntity entity, string[] fieldsToUpdate);
        void SetModified(TEntity entity);
        bool IsDetached(TEntity entity);
        void Detach(TEntity entity);
        void PersistChangesToTrackedEntities();

        //  IEnumerable<TEntity> GetPaginatedData(int page, int pageSize, string sortField, string sortOrder, string searchString, string searchStringUpon);
    }
}
