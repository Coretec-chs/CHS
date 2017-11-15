using Omu.Encrypto;
using Infra;
using Core.Service;
using Service;

namespace WebUI.App_Start
{
    public class WindsorConfig
    {
        public static void Configure()
        {
            WindsorRegistrar.Register(typeof(IHasher), typeof(Hasher));
            WindsorRegistrar.Register(typeof(INavService), typeof(NavService));

            WindsorRegistrar.RegisterAllFromAssemblies("Data");
            WindsorRegistrar.RegisterAllFromAssemblies("Service");
            WindsorRegistrar.RegisterAllFromAssemblies("WebUI");
        }
    }
}