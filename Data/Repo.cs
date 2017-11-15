using Core.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Model;
using System.Data.Entity;
using Core;
using Omu.ValueInjecter;

namespace Data
{
    public class Repo<T> : IRepo<T> where T : Entity, new()
    {
        protected readonly DbContext dbContext;

        public Repo(IDbContextFactory f)
        {
            dbContext = f.GetContext();
        }
        public void Delete(T o)
        {
            dbContext.Set<T>().Remove(o);
        }

        public T Get(string id)
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity == null) throw new CustomException("Record of Type " + entity.GetType().ToString() + "doesn't exist!");
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().AsNoTracking();
        }

        public T Insert(T o)
        {
            var t = dbContext.Set<T>().Create();
            t.InjectFrom(o);
            dbContext.Set<T>().Add(t);
            return t;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate).AsNoTracking();
        }
    }
}
