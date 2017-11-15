using Core.NavModel;
using Core.Service;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using WebUI.Mappers;
using WebUI.Models.Inputs.Leave;
using WebUI.Utility;
using System;
using Core;

namespace WebUI.Controllers
{
    public class LeaveReimburseController : NavCruder<HRLeaveReimbursmentCard, LeaveReimburseViewModel>
    {
        protected readonly IWSService wsService;
        public LeaveReimburseController(INavService navService, IProMapper v, IWSService wsService)
            : base(navService, v)
        {
            this.wsService = wsService;
        }

        public override string EntityKeys(HRLeaveReimbursmentCard entity)
        {
            var keyVals = new object[] { entity.Application_Code };
            return string.Join(",", keyVals);
        }

        public override HRLeaveReimbursmentCard FindEntity(LeaveReimburseViewModel input)
        {
            var entity = navService.Get<HRLeaveReimbursmentCard>(m => m.Application_Code == input.Application_Code);
            return entity;
        }

        public override HRLeaveReimbursmentCard FindEntity(object[] keyValue)
        {
            var appcode = keyValue[0].ToString();
            var entity = navService.Get<HRLeaveReimbursmentCard>(m => m.Application_Code == appcode);
            return entity;
        }

        // GET: Employee
        public async Task<ActionResult> Index(object filterValue = null)
        {
            var userName = User.Identity.Name;
            var model = await navService.WhereAsync<HRLeaveReimbursmentCard>(m => m.Employee_No == userName
                && (m.Status != "Approved" || m.Status != "Rejected"));
            return View(model);
        }

        public ActionResult Submit(string id)
        {
            try
            {
                var entity = navService.Get<HRLeaveApplicationCard>(m => m.Application_Code == id);
                SendApprovalRequest(entity.Application_Code);
                TempData["Message"] = "Saving,Leave Reimbursement Sent for Appproval.,success";
            }
            catch (Exception ex)
            {

                TempData["Message"] = "Error,Error Occurred!" + ex.Message + ",error";
            }

            return RedirectToAction("Index");
        }

        public override LeaveReimburseViewModel DefaultValuesCreateGet(LeaveReimburseViewModel input, object searchValue = null)
        {
            var emp = navService.Get<EmployeeCard>(m => m.No == User.Identity.Name);
            var rcentre = navService.Get<ResponsibilityCenters>(m => m.Code == emp.Responsibility_Center);
            input.Employee_No = emp.No;
            input.Names = string.Concat(emp.First_Name, " ", emp.Last_Name);
            input.Application_Date = DateTime.Today;
            input.Responsibility_Center = emp.Responsibility_Center;
            input.Responsibility_Center_name = rcentre.Name;
            var gender = emp.Gender == null || string.IsNullOrWhiteSpace(emp.Gender) ? "Both" : emp.Gender;
            input.Status = "Draft";
            return input;
        }

        public override LeaveReimburseViewModel DefaultValuesEditGet(LeaveReimburseViewModel input)
        {
            var rcentre = navService.Get<ResponsibilityCenters>(m => m.Code == input.Responsibility_Center);
            input.Responsibility_Center_name = rcentre.Name;
            return input;
        }

        public override LeaveReimburseViewModel DefaultValuesCreatePost(LeaveReimburseViewModel input)
        {
            input.Status = "New";
            return input;
        }

        public override Task<object[]> LogicAfterEdit(HRLeaveReimbursmentCard entity, bool diplayMessage = false, string action = null)
        {
            if (action == "Send for Approval")
            {
                SendApprovalRequest(entity.Application_Code);
                return Task.FromResult(new Object[] { true, "Leave Reimbursement Sent for Appproval." });
            }
            return base.LogicAfterEdit(entity, diplayMessage, action);
        }

        public void SendApprovalRequest(string ApplicationCode)
        {
            try
            {
                var userid = User.Identity.Name;
                var user = navService.Get<EmployeeCard>(m => m.No == userid);
                if (ApplicationCode != null)
                    wsService.PortalWebService().LeaveReimbursment_SendApprovalRequest(ApplicationCode, user.User_ID);
            }
            catch (Exception ex)
            {

                throw new CustomException(ex.Message);
            }
        }

        public async Task<ActionResult> LeaveGrid(string id)
        {
            var userName = User.Identity.Name;
            var lvapptask = navService.WhereAsync<HRLeaveApplicationCard>(m => m.Applicant_Staff_No == userName
                && (m.Status == "Approved"));

            var lvreimbtask = navService.WhereAsync<HRLeaveReimbursmentCard>(m => m.Employee_No == userName
                && m.Status != "Rejected" && m.Application_Code != id);

            await Task.WhenAll(lvapptask, lvapptask);

            var lvapp = lvapptask.Result;
            var lvreimb = lvreimbtask.Result;
            var lvreimbsum = lvapp.GroupJoin(lvreimb, app => app.Application_Code, reimb => reimb.Leave_Application_No,
                (x, y) => new { app = x, reimb = y })
                .SelectMany(x => x.reimb.DefaultIfEmpty(), (x, y) => new { app = x.app, reimb = y })
                .GroupBy(g => g.app)
                .Select(s => new HRLeaveReimbursmentCard()
                {
                    Leave_Application_No = s.Key.Application_Code,
                    Employee_No = s.Key.Applicant_Staff_No,
                    Leave_Type = s.Key.Leave_Type,
                    Start_Date = s.Key.Start_Date,
                    Return_Date = s.Key.Return_Date,
                    Days_Applied = s.Key.Days_Applied,
                    Days_to_Reimburse = s.Key.Days_Applied - s.Sum(m => m.reimb==null? 0: m.reimb.Days_to_Reimburse ?? decimal.Zero)
                }).Where(m => m.Days_to_Reimburse > 0);

            return PartialView(lvreimbsum);

        }
    }
}