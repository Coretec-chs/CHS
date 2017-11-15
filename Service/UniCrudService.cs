using Core;
using Core.Model;
using Core.Repository;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UniCrudService : IUniCrudService
    {
        protected IUniRepo repo;

        public UniCrudService(IUniRepo repo)
        {
            this.repo = repo;
        }
        public string Create<T>(T item) where T : Entity, new()
        {
            try
            {
                var newItem = repo.Insert<T>(item);
                //repo.Save();
                return newItem.Id;
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message); ;
            }

        }

        public void Save()
        {
            repo.Save();
        }

        public T Get<T>( string id) where T : Entity
        {
            return repo.Get<T>(id);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return repo.GetAll<T>();
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : Entity
        {
            return repo.Where<T>(predicate);
        }

        public void Delete<T>(string id) where T : Entity
        {
            repo.Delete<T>(repo.Get<T>(id));
            //repo.Save();
        }
    }
}
