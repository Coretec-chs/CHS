using System;
using System.Web.Configuration;
using System.Net;
using Core.NavModel;
using Core.NavPortalWS;

namespace Data
{
    public class NavConnector : INavConnector
    {
        protected NAV nav;
        protected PortalWebService portalWS;
        private static string NAVPassword = Convert.ToString(WebConfigurationManager.AppSettings["NAVPassword"]);
        private static string NAVUsername = Convert.ToString(WebConfigurationManager.AppSettings["NAVUsername"]);
        private static string NAVODataUri = Convert.ToString(WebConfigurationManager.AppSettings["NAVODataUri"]);
        private static string NAVPortalWSUrl = Convert.ToString(WebConfigurationManager.AppSettings["NAVPortalWSUrl"]);

        public NavConnector()
        {
            
        }

        public NAV GetODataService()
        {
            this.nav = new NAV(new Uri(NAVODataUri))
            {
                Credentials = new NetworkCredential(NAVUsername, NAVPassword)
            };
            return nav;
        }

        public PortalWebService GetPortalWService()
        {
            this.portalWS = new PortalWebService() {
                Url = NAVPortalWSUrl,
                Credentials = new NetworkCredential(NAVUsername, NAVPassword)
            };
            return portalWS;
        }
    }
}
