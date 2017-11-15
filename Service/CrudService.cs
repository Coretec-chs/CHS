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
    public class CrudService<T> : ICrudService<T> where T : Entity, new()
    {
        protected IRepo<T> repo;

        public CrudService(IRepo<T> repo)
        {
            this.repo = repo;
        }

        public IEnumerable<T> GetAll()
        {
            return repo.GetAll();
        }

        public T Get(string id)
        {
            return repo.Get(id);
        }

        public void Insert(T item)
        {
            repo.Insert(item);
        }

        public virtual string Create(T item)
        {
            var newItem = repo.Insert(item);
            repo.Save();
            return newItem.Id;
        }

        public void Save()
        {
            repo.Save();
        }

        public virtual void Delete(string id)
        {
            repo.Delete(repo.Get(id));
            repo.Save();
        }

        public void BatchDelete(string[] ids)
        {
            foreach (var id in ids)
            {
                repo.Delete(repo.Get(id));
            }

            repo.Save();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return repo.Where(predicate);
        }
    }
}
