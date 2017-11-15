using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Service
{
    public interface ICrudService<T> where T : Entity, new()
    {
        void Insert(T item);

        string Create(T item);

        void Save();

        void Delete(string id);

        T Get(string id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Where(Expression<Func<T, bool>> func);

    }
}
