using Foolproof;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebUI.ViewModels.Inputs;

namespace WebUI.Models.Inputs.Leave
{
    public class LeaveReimburseViewModel : Input
    {
        [DisplayName("Application Code")]
        public string Application_Code { get; set; }

        [DisplayName("Application Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [UIHint("Text")]
        public DateTime? Application_Date { get; set; }

        [DisplayName("Days Applied")]
        public decimal? Days_Applied { get; set; }

        [Required]
        [DisplayName("Days to Reimburse")]
        [LessThanOrEqualTo("Max_to_Reimburse", ErrorMessage = "Reimburse days exceed days available")]
        [Range(0.5, double.MaxValue, ErrorMessage = ("Days to reimburse must be greater than Zero"))]
        public decimal? Days_to_Reimburse { get; set; }

        [DisplayName("Employee No")]
        public string Employee_No { get; set; }

        [DisplayName("Job Title")]
        public string Job_Tittle { get; set; }

        [Required]
        [DisplayName("Select Leave Application")]
        [UIHint("ModalButton")]
        public string Leave_Application_No { get; set; }

        [DisplayName("Leave Type")]
        public string Leave_Type { get; set; }

        [DisplayName("Names")]
        public string Names { get; set; }

        [DisplayName("Responsibility Center")]
        public string Responsibility_Center { get; set; }

        [DisplayName("Return Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [UIHint("Text")]
        public DateTime? Return_Date { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [UIHint("Text")]
        public DateTime? Start_Date { get; set; }

        [Display(Name = "Responsibility Center")]
        public string Responsibility_Center_name { get; set; }

        private decimal? max_days;
        [Required]
        [DisplayName("Max. to Reimburse")]
        public decimal? Max_to_Reimburse
        {
            get
            {
                if (max_days == null || max_days == decimal.Zero)
                    max_days = Days_to_Reimburse;
                return max_days;
            }
            set
            {
                max_days = value;
            }
        }

    }
}