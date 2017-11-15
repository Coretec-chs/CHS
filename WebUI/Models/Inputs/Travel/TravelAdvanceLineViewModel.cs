using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.ViewModels.Inputs;
using Core.NavModel;
using System.Web.Mvc;
using WebUI.Utility;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Inputs.Travel
{
    public class TravelAdvanceLineViewModel :Input
    {
        public string Id { get; set; }

        public string Account_No { get; set; }

        public string No { get; set; }

        public string Global_Dimension_1_Code { get; set; }

        public string Purpose { get; set; }

        public string Shortcut_Dimension_2_Code { get; set; }

        public string Account_Name { get; set; }

        [Required]
        [Display(Name ="Type")]
        public string Advance_Type { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public decimal Amount_LCY { get; set; }

        [Required]
        [Display(Name = "Daily Rate")]
        public decimal Daily_Rate_Amount { get; set; }

        [Required]
        [Display(Name = "Begin Date")]
        public DateTime Date_Issued { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Destination_Code { get; set; }
        public DateTime Due_Date { get; set; }
        public string Imprest_Holder { get; set; }
        public int Line_No { get; set; }

        [Required]
        [Display(Name = "No of Days")]
        public int No_of_Days { get; set; }

        public bool deleted { get; set; }
        public IEnumerable<SelectListItem> AdvanceTypeList
        {
            get
            {
                var list = DropDownLists.PopulateList("advanceType");
                return list;
            }
        }

        public IEnumerable<SelectListItem> DestinationCodeList
        {
            get
            {
                var list = DropDownLists.PopulateList("destinationCode");
                return list;
            }
        }

    }
}