using Core.NavModel;
using Core.Service;
using System.Threading.Tasks;
using System.Data.Services.Client;
using System.Web.Mvc;
using WebUI.Mappers;
using WebUI.Models.Inputs.Leave;
using System;
using System.Linq;
using WebUI.Utility;
using Core;

namespace WebUI.Controllers
{
    [Authorize]
    public class LeaveApplicationController : NavCruder<HRLeaveApplicationCard, LeaveApplicationViewModel>
    {
        protected readonly IWSService wsService;
        public LeaveApplicationController(INavService navService, IProMapper v, IWSService wsService)
            : base(navService, v)
        {
            this.wsService = wsService;
        }

        // GET: Employee
        public async Task<ActionResult> Index(object filterValue = null)
        {
            var userName = User.Identity.Name;
            var model = await navService.WhereAsync<HRLeaveApplicationCard>(m => m.Applicant_Staff_No == userName 
                && (m.Status != "Approved" || m.Status != "Rejected") );
            return View(model);
        }

        public ActionResult Submit(string id)
        {
            try
            {
                var entity = navService.Get<HRLeaveApplicationCard>(m => m.Application_Code == id);
                SendApprovalRequest(entity.Application_Code);
                TempData["Message"] = "Saving,Leave Application Sent for Appproval.,success";
            }
            catch (Exception ex)
            {

                TempData["Message"] = "Error,Error Occurred!" + ex.Message + ",error";
            }

            return RedirectToAction("Index");
        }

        public override HRLeaveApplicationCard FindEntity(object[] keyValue)
        {
            var appcode = keyValue[0].ToString();
            var entity = navService.Get<HRLeaveApplicationCard>(m => m.Application_Code == appcode);
            return entity;
        }

        public override HRLeaveApplicationCard FindEntity(LeaveApplicationViewModel input)
        {
            var entity = navService.Get<HRLeaveApplicationCard>(m => m.Application_Code == input.Application_Code);
            return entity;
        }
        public override LeaveApplicationViewModel DefaultValuesCreateGet(LeaveApplicationViewModel input, object searchValue = null)
        {
            var emp = navService.Get<EmployeeCard>(m => m.No == User.Identity.Name);
            var rcentre = navService.Get<ResponsibilityCenters>(m => m.Code == emp.Responsibility_Center);
            var usersetup = navService.Get<UserSetup>(m => m.User_ID == emp.User_ID);
            //var approver = navService.Get<EmployeeCard>(m => m.No == usersetup.Approver_ID);
            input.Applicant_Staff_No = emp.No;
            input.Names = string.Concat(emp.First_Name, " ", emp.Last_Name);
            input.Application_Date = DateTime.Today;
            input.Responsibility_Center = emp.Responsibility_Center;
            input.Responsibility_Center_name = rcentre.Name;
            input.Applicant_Supervisor = usersetup.Approver_ID;
            //input.Applicant_Supervisor_User = string.Concat(approver.First_Name, " ", approver.Last_Name);
            //input.Supervisor_Email = approver.Company_E_Mail;
            var gender = emp.Gender == null || string.IsNullOrWhiteSpace(emp.Gender) ? "Both" : emp.Gender;
            input.LeaveTypeList = DropDownLists.PopulateList("leavetype", gender);
            input.Status = "Draft";
            return input;
        }

        public override LeaveApplicationViewModel DefaultValuesEditGet(LeaveApplicationViewModel input)
        {
            var emp = navService.Get<EmployeeCard>(m => m.No == User.Identity.Name);
            var rcentre = navService.Get<ResponsibilityCenters>(m => m.Code == emp.Responsibility_Center);
            var gender = emp.Gender == null || string.IsNullOrWhiteSpace(emp.Gender) ? "Both" : emp.Gender;
            input.Responsibility_Center_name = rcentre.Name;
            input.LeaveTypeList = DropDownLists.PopulateList("leavetype", gender);
            return input;
        }

        public override LeaveApplicationViewModel DefaultValuesCreatePost(LeaveApplicationViewModel input)
        {
            input.Status = "New";
            return input;
        }

        public override Task<object[]> LogicAfterEdit(HRLeaveApplicationCard entity, bool diplayMessage = false, string action = null)
        {
            if (action == "Send for Approval")
            {
                SendApprovalRequest(entity.Application_Code);
                return Task.FromResult(new Object[] { true, "Leave Sent for Appproval." });
            }
            return base.LogicAfterEdit(entity, diplayMessage, action);
        }


        public JsonResult GetReturnDate(string leaveType, double appliedDays, DateTime startDate)
        {
            DateTime returnDate = startDate;
            var ltypes = navService.Get<HRLeaveTypeCard>(m => m.Code == leaveType);
            var calenderLines = navService.Where<HRLeaveCalenderLines>(m => m.Date >= startDate && m.Non_Working == true).ToList();
            if (ltypes.Inclusive_of_Non_Working_Days != true)
            {
                int i = 1;
                while (i <= appliedDays)
                {
                    if (!calenderLines.Exists(m => m.Date == returnDate))
                    {
                        i++;
                    }
                    returnDate = returnDate.AddDays(1);
                }
            }
            else
            {
                returnDate = startDate.AddDays(appliedDays);
                while (calenderLines.Exists(m => m.Date == returnDate))
                {
                    returnDate = returnDate.AddDays(1);
                }
            }
            var rDate = returnDate.ToString("dd MMM yyyy");
            var data = new
            {
                returnDate = rDate
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void SendApprovalRequest(string ApplicationCode)
        {
            try
            {
                var userid = User.Identity.Name;
                var user = navService.Get<EmployeeCard>(m => m.No == userid);
                if (ApplicationCode != null)
                    wsService.PortalWebService().SendLeaveForApproval(ApplicationCode, user.User_ID);
            }
            catch (Exception ex)
            {

                throw new CustomException(ex.Message);
            }
        }

        public override string EntityKeys(HRLeaveApplicationCard entity)
        {
            var keyVals = new object[] { entity.Application_Code };
            return string.Join(",", keyVals);
        }
    }
}