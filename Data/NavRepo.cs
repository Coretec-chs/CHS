using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.NavModel;
using System.Linq.Expressions;
using Omu.ValueInjecter;
using LinqKit;
using System.Reflection;
using System.Data.Services.Client;

namespace Data
{
    public class NavRepo : INavRepo
    {
        private readonly NAV nav;

        public NavRepo(INavConnector conn)
        {
            nav = conn.GetODataService();
        }
        public void Delete<T>(T o) where T : class
        {
            nav.DeleteObject(o);
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return nav.CreateQuery<T>(typeof(T).Name);
        }

        public T Insert<T>(T o) where T : class, new()
        {
            var t = new T();
            t.InjectFrom(o);
            nav.AddObject(typeof(T).Name, o);
            return t;
        }

        public void Save()
        {
            nav.SaveChanges();
        }

        public void Update<T>(T o) where T : class
        {
            nav.UpdateObject(o);
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return nav.CreateQuery<T>(typeof(T).Name).Where(predicate);
        }
    }
}
