using Core.NavModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Utility;
using WebUI.ViewModels.Inputs;

namespace WebUI.Models.Inputs.Leave
{
    public class LeaveApplicationViewModel : Input
    {

        [Display(Name = "Total Leave Days")]
        public decimal? Total_Leave_Days { get; set; }

        [Display(Name = "Total Leave Taken")]
        public decimal? Total_Leave_Taken { get; set; }

        [Display(Name = "Leave Balance")]
        public decimal? Leave_Balance { get; set; }

        [Display(Name = "Allocated Leave Days")]
        public decimal? Allocated_Leave_Days { get; set; }

        [Display(Name = "Reimbursed Leave Days")]
        public decimal? Reimbursed_Leave_Days { get; set; }

        [Display(Name = "Application Code")]
        public string Application_Code { get; set; }

        [Display(Name = "Application Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [UIHint("Text")]
        public DateTime? Application_Date { get; set; }
        

        [Required]
        [Display(Name = "Days Applied")]
        public decimal? Days_Applied { get; set; }

        [Display(Name = "Employee No")]
        public string Applicant_Staff_No { get; set; }

        [Display(Name = "Employee Name")]
        
        public string Names { get; set; }


        [Display(Name = "End Date")]
        public DateTime? End_Date { get; set; }

        [Required]
        [Display(Name = "Leave Type")]
        [UIHint("dropdown")]
        public string Leave_Type { get; set; }

        [Display(Name = "Return Date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Return_Date { get; set; }
        

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Start_Date { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Station Code")]
        public string Station_Code { get; set; }

        [Display(Name = "Request Leave Allowance")]
        public bool Request_Leave_Allowance { get; set; }

        [Required]
        [Display(Name = "Reliever")]
        [UIHint("dropdown")]
        public string Reliever { get; set; }

        [Display(Name = "Job Title")]
        public string Job_Tittle { get; set; }

        [Required]
        [Display(Name = "Applicant Supervisor")]
        public string Applicant_Supervisor { get; set; }

        [Display(Name = "Supervisor Email")]
        public string Supervisor_Email { get; set; }

        [Display(Name = "Applicant Comments")]
        [UIHint("Multiline")]
        public string Applicant_Comments { get; set; }

        [Display(Name = "Reliever Name")]
        public string Reliever_Name { get; set; }

        public string Responsibility_Center { get; set; }

        [Display(Name = "Responsibility Center")]
        public string Responsibility_Center_name { get; set; }

        [Display(Name = "Days Available")]
        public decimal? D_Available { get; set; }

        public IEnumerable<SelectListItem> RelieverList
        {
            get
            {
                var list = DropDownLists.PopulateList("employee");
                return list;
            }
        }

        public IEnumerable<SelectListItem> LeaveTypeList { get; set; }

        public IList<Approval_Comment_Line> Approval_Comment_Lines { get; set; }
    }
}