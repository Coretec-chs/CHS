using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class IoC
    {
        private static readonly IWindsorContainer TheContainer = new WindsorContainer();

        public static IWindsorContainer Container
        {
            get { return TheContainer; }
        }

        public static T Resolve<T>()
        {
            return TheContainer.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return TheContainer.Resolve(type);
        }
    }
}
