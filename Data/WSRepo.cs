
using Core.NavPortalWS;
using Core.Repository;

namespace Data
{
    public class WSRepo: IWSRepo
    {
        protected readonly INavConnector conn;

        public WSRepo(INavConnector conn)
        {
            this.conn = conn;
        }

        public PortalWebService PortalWebService()
        {
            return conn.GetPortalWService();
        }
    }
}
