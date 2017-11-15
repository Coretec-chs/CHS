using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Repository
{
    public interface IUniRepo
    {
        T Insert<T>(T o) where T : Entity, new();
        void Save();
        T Get<T>(string id) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : Entity;
        void Delete<T>(T o) where T : Entity;
    }
}
