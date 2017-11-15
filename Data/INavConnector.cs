

using Core.NavModel;
using Core.NavPortalWS;

namespace Data
{
    public interface INavConnector
    {
        NAV GetODataService();

        PortalWebService GetPortalWService();
    }
}
