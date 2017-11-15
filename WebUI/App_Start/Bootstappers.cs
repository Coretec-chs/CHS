using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Infra;
using WebUI.Mappers;
using WebUI.Utility;

namespace WebUI.App_Start
{
    public class Bootstrappers
    {
        public static void Bootstrap()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            WindsorConfig.Configure();
            MapperConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //BootstrapEditorTemplatesConfig.RegisterBundles();

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}