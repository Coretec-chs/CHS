using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebUI.Utility;
using WebUI.ViewModels.Inputs;

namespace WebUI.Models.Inputs.Travel
{
    public class TravelAdvanceViewModel : Input
    {
        [DisplayName("Employee No")]
        public string Account_No { get; set; }

        public string Responsibility_Center { get; set; }

        [DisplayName("Responsibility Center")]
        public string Responsibility_Center_Name { get; set; }

        [DisplayName("Application No.")]
        public string No { get; set; }

        public string ETag { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        // [Required]
        [DisplayName("Account Type")]
        public string Account_Type { get; set; }

        public string Bank_Name { get; set; }

        public string Budget_Center_Name { get; set; }

        // [Required]
        [DisplayName("Requestor ID")]
        public string Cashier { get; set; }

        public string Cheque_No { get; set; }

        public string Currency_Code { get; set; }

        [Required]
        [DisplayName("Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Date { get; set; }

        public string Dim3 { get; set; }

        public string Dim4 { get; set; }

        public string Function_Name { get; set; }

        [Required]
        [DisplayName("Donor")]
        [UIHint("cascadedropdown")]
        public string Global_Dimension_1_Code { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]

        public DateTime? Imprest_Due_Date { get; set; }

        public string Lowvalue_Request_No { get; set; }

        public string Pay_Mode { get; set; }

        // [Required]
        [DisplayName("Names")]
        public string Payee { get; set; }

        // [Required]
        [DisplayName("Paying Bank Account")]
        public string Paying_Bank_Account { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Payment_Release_Date { get; set; }

        [Required]
        [DisplayName("Purpose")]
        [UIHint("Multiline")]
        public string Purpose { get; set; }

        [Required]
        [DisplayName("Project")]
        [UIHint("dropdown")]
        public string Shortcut_Dimension_2_Code { get; set; }

        [Required]
        [DisplayName("Program")]
        [UIHint("dropdown")]
        public string Shortcut_Dimension_3_Code { get; set; }

        [Required]
        [DisplayName("County")]
        [UIHint("dropdown")]
        public string Shortcut_Dimension_5_Code { get; set; }

        [Required]
        [DisplayName("Activity")]
        [UIHint("dropdown")]
        public string Shortcut_Dimension_4_Code { get; set; }

        public decimal? Total_Net_Amount { get; set; }

        public decimal? Total_Net_Amount_LCY { get; set; }       


        public IEnumerable<SelectListItem> ProjectList
        {
            get
            {
                var list = new List<SelectListItem>();
                return list;
            }
        }

        public IEnumerable<SelectListItem> ProgramAreaList
        {
            get
            {
                var list = DropDownLists.PopulateList("dimension", "PROGRAMAREA");
                return list;
            }
        }

        public IEnumerable<SelectListItem> DonorList
        {
            get
            {
                var list = DropDownLists.PopulateList("dimension", "DONOR");
                return list;
            }
        }

        public IEnumerable<SelectListItem> ActivityList
        {
            get
            {
                var list = DropDownLists.PopulateList("dimension", "Activity");
                return list;
            }
        }

        public IEnumerable<SelectListItem> CountyList
        {
            get
            {
                var list = DropDownLists.PopulateList("dimension", "COUNTY");
                return list;
            }
        }

        public IEnumerable<SelectListItem> CurrencyList
        {
            get
            {
                var list = DropDownLists.PopulateList("currency");
                return list;
            }
        }
    }

    public class TravelAdvanceDetails
    {
        public static IList<TravelAdvanceLineViewModel> TravelAdvanceLineList = null;

        public static IList<TravelAdvanceLineViewModel> GetTravelAdvanceLineList()
        {
            if (TravelAdvanceLineList == null)
            {
                TravelAdvanceLineList = new List<TravelAdvanceLineViewModel>();
            }
            return TravelAdvanceLineList;
        }
    }
}