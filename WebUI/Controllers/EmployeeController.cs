using Core.NavModel;
using Core.Service;
using System.Linq;
using System.Web.Mvc;
using WebUI.Mappers;
using WebUI.Models.Inputs.Employee;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace WebUI.Controllers
{
    [Authorize]
    public class EmployeeController : NavCruder<EmployeeCard, EmployeeViewModel>
    {
        public EmployeeController(INavService navService, IProMapper v)
            : base(navService, v)
        {

        }

        public override string EntityKeys(EmployeeCard entity)
        {
            throw new NotImplementedException();
        }

        public override EmployeeCard FindEntity(EmployeeViewModel input)
        {
            throw new NotImplementedException();
        }

        public override EmployeeCard FindEntity(object[] keyValue)
        {
            throw new NotImplementedException();
        }

        // GET: Employee
        public async Task<ActionResult> Index(object filterValue = null)
        {
            var userName = User.Identity.Name;

            var entity = await navService.GetAsync<EmployeeCard>(m => m.No == userName);
            var model = mapper.Map<EmployeeCard, EmployeeViewModel>(entity, new EmployeeViewModel());
            return View(model);
        }
        
    }
}