using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore.DataAccess.Bases.EntityFramework
{
    public abstract class RepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity, TDbContext> where TEntity : RecordBase, new() where TDbContext : DbContext, new()
    {
        public TDbContext DbContext { get; set; }
        protected RepositoryBase()
        {
            DbContext = new TDbContext();       
        }
        protected RepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext;    
        }
        public void Add(TEntity entity , bool save = true)
        {
            entity.Guid = Guid.NewGuid().ToString();
            entity.CreateDate = DateTime.Now;
            DbContext.Set<TEntity>().Add(entity);

            if (save)
            {
                Save();
            }
        }

        public void Delete(TEntity entity, bool softDelete = false,bool save = true)
        {
            if (softDelete)
            {
                entity.IsDeleted = true;
                Update(entity,save);
            }
            DbContext.Set<TEntity>().Remove(entity);
            if (save)
            {
                Save();
            }
        }
        public virtual void DeleteEntity(int id, bool save =true,bool softDelete = false)
        {
            var entity = DbContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
            Delete(entity,save,softDelete);
        }

        public virtual void DeleteEntity(string guid, bool save = true, bool softDelete = false)
        {
            var entity = DbContext.Set<TEntity>().SingleOrDefault(e => e.Guid == guid);
            Delete(entity, save, softDelete);
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }

        public IQueryable<TEntity> Query()
        {
            var query = DbContext.Set<TEntity>().AsQueryable();
            // Sadece silinmemiş kayıtları getirir.
            // query = query.Where(q => q.IsDeleted == null || q.IsDeleted == false);
            query = query.Where(q => q.IsDeleted ?? false == false);

            return query;   
        }
        public virtual IQueryable<TEntity>EntityQuery(params string[] entitiesToInclude)
        {
            var query = Query();

            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }
           return query;
        }
        public virtual IQueryable<TEntity> EntityQuery(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            var query = EntityQuery(entitiesToInclude);
            query = query.Where(predicate);
            return query;
        }
        

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public void Update(TEntity entity, bool save = true)
        {
            entity.UpdateDate = DateTime.Now;
            DbContext.Set<TEntity>().Update(entity);
            if (save)
            {
                Save();
            }
        }
    }
}
