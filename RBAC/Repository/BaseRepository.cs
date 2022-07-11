using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rbac.Entity;
using Z;
using System.Linq.Expressions;

namespace Repository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity,TKey>
        where TKey : struct
        where TEntity : class
    {
        private readonly RbacDbContext dbContext;

        public BaseRepository(RbacDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            return dbContext.SaveChanges();
            
        }

        public int Add(List<TEntity> entity)
        {
            dbContext.Set<TEntity>().AddRange(entity);
            return dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return dbContext.SaveChanges();
        }

        public int Delete(TKey key)
        {
            dbContext.Set<TEntity>().DeleteByKey(key);
            return dbContext.SaveChanges();
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.Set<TEntity>().Where(predicate).DeleteFromQuery();
            return dbContext.SaveChanges();
        }

        public List<TEntity> FindAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity FindOne(TKey key)
        {
            var list= dbContext.Set<TEntity>().Find(key);
            return dbContext.Set<TEntity>().Find(key);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public IQueryable<TEntity> QueryList(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }
        public IQueryable<TEntity> QueryList()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public int Update(TEntity entity)
        {
            dbContext.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Update(TEntity entity, params string[] ps)
        {
            var en = dbContext.Entry<TEntity>(entity);
            foreach (string s in ps)
            {
                en.Property(s).IsModified = true;
            }
            return dbContext.SaveChanges();
        }
    }
}
