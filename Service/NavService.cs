using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core;
using Core.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace Service
{
    public class NavService : INavService
    {

        protected INavRepo repo;

        public NavService(INavRepo repo)
        {
            this.repo = repo;
        }
        public T Create<T>(T item) where T : class, new()
        {
            try
            {
                var newItem = repo.Insert<T>(item);
                return newItem;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public Task<T> CreateAsync<T>(T item) where T : class, new()
        {
            try
            {
                repo.Insert(item);
                //repo.Save();
                return Task.Run(() => item);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public void Delete<T>(T o) where T : class
        {
            repo.Delete<T>(o);
            repo.Save();
        }

        public Task DeleteAsync<T>(T o) where T : class
        {
           return Task.Run(() =>
            {
                repo.Delete<T>(o);
                repo.Save();
            });
        }

        public T Get<T>(Expression<Func<T, bool>> func) where T : class
        {
            try
            {
                return repo.Where<T>(func).First();
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
           
        }

        public Task<T> GetAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            try
            {

                return Task.Run(() => repo.Where<T>(func).First());
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return repo.GetAll<T>();
        }

        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return Task.Run(() => repo.GetAll<T>());
        }        
       

        public void Save()
        {
            repo.Save();
        }

        public Task SaveAsync()
        {
            return Task.Run(() => repo.Save());
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> func) where T : class
        {
            return repo.Where<T>(func);
        }

        public Task<IEnumerable<T>> WhereAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            return Task.Run(() => repo.Where<T>(func));
        }

        public T Update<T>(T o) where T : class
        {
             repo.Update<T>(o);
            return o;
        }

        public Task<T> UpdateAsync<T>(T o) where T : class
        {
            repo.Update<T>(o);
            return Task.Run(() => o);
        }
    }
}
