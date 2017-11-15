using Core.NavModel;
using Core.Service;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected readonly INavService service;

        public HomeController(INavService service)
        {
            this.service = service;
        }
        public async Task<ActionResult> Index()
        {
            var model = await service.GetAsync<EmployeeCard>(m => m.No == User.Identity.Name);
            return View(model);
        }

    }
}