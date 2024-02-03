using DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContext db;
        protected DbSet<TEntity> context;

        public BaseRepository(DbContext dbContext)
        {
            db = dbContext;
            context = db.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity entity)
        {

            context.Add(entity);
            db.ChangeTracker.TrackGraph(entity, e =>
            {
                e.Entry.State = EntityState.Added;
            });

            return context.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {

                context.Add(entity);
                db.ChangeTracker.TrackGraph(entity, e =>
                {
                    e.Entry.State = EntityState.Added;
                });
            }

            context.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return context.ToList(); //.AsEnumerable();
        }

        public virtual TEntity GetById(TKey id)
        {
            return context.Find(id);
        }

        public void PatchUpdate(TEntity entity, string[] fieldsToUpdate)
        {
            foreach (var field in fieldsToUpdate)
                db.Entry(entity).Property(field).IsModified = true;
        }

        public void Remove(TKey id)
        {
            TEntity entityToRemove = context.Find(id);
            if (entityToRemove != null)
            {
                Remove(entityToRemove);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            context.Attach(entity);
            var statusProperty = entity.GetType().GetProperty("StatusId");

            if (statusProperty != null)
            {
            }
            else
            {
                context.Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                context.Attach(entity);
            }
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
            }
            SetModified(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (db.Entry(entity).State == EntityState.Detached)
                {
                    context.Attach(entity);
                }
                SetModified(entity);
            }
        }
        public void PersistChangesToTrackedEntities()
        {
            db.SaveChanges();
        }

        public virtual void SetModified(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public virtual bool IsDetached(TEntity entity)
        {
            return db.Entry(entity).State == EntityState.Deleted;
        }

        public virtual void Detach(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Detached;
        }

    }
}
