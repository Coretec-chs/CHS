using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Repository
{
    public interface IRepo<T>
    {
        T Get(string id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        T Insert(T o);
        void Save();
        void Delete(T o);
    }
}
