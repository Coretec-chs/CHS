using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using System.Linq.Expressions;
using System.Data.Entity;
using Omu.ValueInjecter;

namespace Data
{
    public class UniRepo : IUniRepo
    {
        private readonly DbContext c;

        public UniRepo(IDbContextFactory a)
        {
            c = a.GetContext();
        }
        public void Delete<T>(T o) where T : Entity
        {
            c.Set<T>().Remove(o);
        }

        public T Get<T>(string id) where T : Entity
        {
            return c.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return c.Set<T>().AsNoTracking();
        }

        public T Insert<T>(T o) where T : Entity, new()
        {
            var t = new T();
            t.InjectFrom(o);
            c.Set<T>().Add(t);
            return t;
        }

        public void Save()
        {
            c.SaveChanges();
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : Entity
        {
            return c.Set<T>().Where(predicate).AsNoTracking();
        }
    }
}
