using Core.NavModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface INavService
    {
        T Create<T>(T item) where T : class, new();

        T Update<T>(T o) where T : class;

        void Save();

        T Get<T>(Expression<Func<T, bool>> func) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;        

        IEnumerable<T> Where<T>(Expression<Func<T, bool>> func) where T : class;       

        void Delete<T>(T o) where T : class;

        Task<T> CreateAsync<T>(T item) where T : class, new();

        Task<T> UpdateAsync<T>(T o) where T : class;

        Task SaveAsync();

        Task<T> GetAsync<T>(Expression<Func<T, bool>> func) where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;

        Task<IEnumerable<T>> WhereAsync<T>(Expression<Func<T, bool>> func) where T : class;       

        Task DeleteAsync<T>(T o) where T : class;
    }
}
