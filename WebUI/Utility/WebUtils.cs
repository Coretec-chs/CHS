using System.Web;

namespace WebUI.Utility
{
    public static class WebUtils
    {
        public static bool IsUserAdmin()
        {
            return HttpContext.Current.User.IsInRole("admin");
        }
    }
}