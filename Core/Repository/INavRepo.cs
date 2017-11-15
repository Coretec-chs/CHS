using Core.NavModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface INavRepo
    {
        T Insert<T>(T o) where T : class, new();
        void Update<T>(T o) where T : class;
        void Save();
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Delete<T>(T o) where T : class;       
    }
}
