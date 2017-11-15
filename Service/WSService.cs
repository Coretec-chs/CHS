using System;
using Core.NavPortalWS;
using Core.Repository;
using Core.Service;

namespace Service
{
    public class WSService:IWSService
    {
        protected IWSRepo ws;

        public WSService(IWSRepo ws)
        {
            this.ws = ws;
        }

        public PortalWebService PortalWebService()
        {
            return ws.PortalWebService();
        }
    }


}
