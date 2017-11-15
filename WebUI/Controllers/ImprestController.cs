using Core;
using Core.NavModel;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Mappers;
using WebUI.Models.Inputs.Travel;
using WebUI.Utility;

namespace WebUI.Controllers
{
    [Authorize]
    public class ImprestController : NavCruder<TravelAdvanceRequisition, TravelAdvanceViewModel>
    {
        protected readonly IWSService wsService;
        public ImprestController(INavService navService, IProMapper v, IWSService wsService)
            : base(navService, v)
        {
            this.wsService = wsService;
        }

        public override string EntityKeys(TravelAdvanceRequisition entity)
        {
            var keyVals = new object[] { entity.No };
            return string.Join(",", keyVals);
        }

        public override TravelAdvanceRequisition FindEntity(TravelAdvanceViewModel input)
        {
            var entity = navService.Get<TravelAdvanceRequisition>(m => m.No == input.No);
            return entity;
        }

        public override TravelAdvanceRequisition FindEntity(object[] keyValue)
        {
            var appno = keyValue[0].ToString();
            var entity = navService.Get<TravelAdvanceRequisition>(m => m.No == appno);
            return entity;
        }

        // GET: Employee
        public async Task<ActionResult> Index(object filterValue = null)
        {
            var userName = User.Identity.Name;
            var model = await navService.WhereAsync<TravelAdvanceRequisition>(m => m.Account_No == userName
                && (m.Status != "Posted" || m.Status != "Cancelled"));
            return View(model);
        }

        public async Task<ActionResult> Lines(string id = "", bool newList = true)
        {

            if (newList)
            {
                var userName = User.Identity.Name;
                var model = await navService.WhereAsync<TravelAdvanceRequisitionPVLines>(m => m.No == id);
                if (model != null)
                {
                    foreach (var entity in model)
                    {
                        var item = mapper.Map<TravelAdvanceRequisitionPVLines, TravelAdvanceLineViewModel>(entity);
                        item.Id = Guid.NewGuid().ToString();
                        item.deleted = false;
                        TravelAdvanceDetails.TravelAdvanceLineList.Add(item);
                    }
                }
            }
            var modelList = TravelAdvanceDetails.GetTravelAdvanceLineList().Where(m => !m.deleted);


            return PartialView("_Lines", modelList);
        }

        public ActionResult AddLine(String id = "-1")
        {
            TravelAdvanceLineViewModel item = TravelAdvanceDetails.GetTravelAdvanceLineList().FirstOrDefault(m => m.Id == id);
            if (item == null)
                item = new TravelAdvanceLineViewModel() { Id = new Guid().ToString(), deleted = false };

            return PartialView("_AddLine", item);
        }

        [HttpPost]
        public ActionResult AddLine(TravelAdvanceLineViewModel input)
        {
            var returnMessage = String.Empty;
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    returnMessage = "Error," + messages + ",error";

                    var jdata = new
                    {
                        html = PartialView("_AddLine", input).RenderToString(),
                        msg = returnMessage,
                        success = false
                    };
                    return Json(jdata, JsonRequestBehavior.AllowGet);
                }


                TravelAdvanceLineViewModel item = TravelAdvanceDetails.GetTravelAdvanceLineList().FirstOrDefault(m => m.Id == input.Id);
                if (item == null)
                {
                    TravelAdvanceDetails.TravelAdvanceLineList.Add(input);
                }
                else
                {
                    item = input;
                }

                var displaymsg = "Saved Successfully!!";
                returnMessage = "Saving," + displaymsg + ",success";

                var data = new
                {
                    html = PartialView("_AddLine", item).RenderToString(),
                    msg = returnMessage,
                    success = true
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {

                returnMessage = "Error,Error! " + ex.Message.Replace(System.Environment.NewLine, " ") + ",error";

                var data = new
                {
                    html = PartialView("_CreateEng", input).RenderToString(),
                    msg = returnMessage,
                    success = false
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                returnMessage = "Error,Error! " + ex.Message.Replace(System.Environment.NewLine, " ") + ",error";

                var data = new
                {
                    html = PartialView("_CreateEng", input).RenderToString(),
                    msg = returnMessage,
                    success = false
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public override TravelAdvanceViewModel DefaultValuesCreateGet(TravelAdvanceViewModel input, object searchValue = null)
        {
            var emp = navService.Get<EmployeeCard>(m => m.No == User.Identity.Name);
            var rcentre = navService.Get<ResponsibilityCenters>(m => m.Code == emp.Responsibility_Center);
            var usersetup = navService.Get<UserSetup>(m => m.User_ID == emp.User_ID);
            //var approver = navService.Get<EmployeeCard>(m => m.No == usersetup.Approver_ID);
            input.Account_No = emp.No;
            input.Payee = string.Concat(emp.First_Name, " ", emp.Last_Name);
            input.Date = DateTime.Today;
            input.Responsibility_Center = emp.Responsibility_Center;
            input.Responsibility_Center_Name = rcentre.Name;
            input.Cashier = emp.User_ID;
            input.Status = "Draft";
            return input;
        }

        public override TravelAdvanceViewModel DefaultValuesEditGet(TravelAdvanceViewModel input)
        {
            var rcentre = navService.Get<ResponsibilityCenters>(m => m.Code == input.Responsibility_Center);
            input.Responsibility_Center_Name = rcentre.Name;
            return input;
        }

        public override TravelAdvanceViewModel DefaultValuesCreatePost(TravelAdvanceViewModel input)
        {
            input.Status = "Pending";
            return input;
        }

        public override Task<object[]> LogicAfterEdit(TravelAdvanceRequisition entity, bool diplayMessage = false, string action = null)
        {
            if (action == "Send for Approval")
            {
                SendApprovalRequest(entity.No);
                return Task.FromResult(new Object[] { true, "Travel Advance Requisition Sent for Appproval." });
            }
            return base.LogicAfterEdit(entity, diplayMessage, action);
        }

        public ActionResult Submit(string id)
        {
            try
            {
                var entity = navService.Get<HRLeaveApplicationCard>(m => m.Application_Code == id);
                SendApprovalRequest(entity.Application_Code);
                TempData["Message"] = "Saving,Travel Advance Requisition Sent for Appproval.,success";
            }
            catch (Exception ex)
            {

                TempData["Message"] = "Error,Error Occurred!" + ex.Message + ",error";
            }

            return RedirectToAction("Index");
        }

        public void SendApprovalRequest(string ApplicationCode)
        {
            try
            {
                var userid = User.Identity.Name;
                var user = navService.Get<EmployeeCard>(m => m.No == userid);
                if (ApplicationCode != null)
                    wsService.PortalWebService().Travel_SendApprovalRequest(ApplicationCode, user.User_ID);
            }
            catch (Exception ex)
            {

                throw new CustomException(ex.Message);
            }
        }


        public JsonResult GetProject(string Origin, string Target, string Value)
        {
            if (String.IsNullOrWhiteSpace(Value)) Value = "-1";
            IEnumerable<SelectListItem> projectlist = DropDownLists.PopulateList("dimension", "PROJECT", Value);
            return Json(projectlist);
        }

        public JsonResult GetReturnDate(string advanceType, string destination)
        {
            var model = navService.Get<DestinationRateList>(m => m.Advance_Code == advanceType && m.Destination_Code == destination);

            var dailyRate = model.Daily_Rate_Amount.Value.ToString("N2");
            var data = new
            {
                dailyRate = dailyRate
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}