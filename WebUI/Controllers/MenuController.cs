using Core.Model;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class MenuController : Controller
    {
        protected readonly IUniCrudService service;

        public MenuController(IUniCrudService service)
        {
            this.service = service;
        }

        // GET: Menu
        public ActionResult Index()
        {
            var model = service.GetAll<AspNetMenus>();
            return PartialView(model);
        }
    }
}