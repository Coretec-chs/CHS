using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Service
{
    public interface IUniCrudService
    {
        string Create<T>(T item) where T : Entity, new();

        void Save();

        T Get<T>(string id) where T : Entity;

        IEnumerable<T> GetAll<T>() where T : Entity;

        IEnumerable<T> Where<T>(Expression<Func<T, bool>> func) where T : Entity;

        void Delete<T>(string id) where T : Entity;
    }
}
