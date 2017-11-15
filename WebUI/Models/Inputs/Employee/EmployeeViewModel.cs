using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebUI.Utility;
using WebUI.ViewModels.Inputs;

namespace WebUI.Models.Inputs.Employee
{
    
    public class EmployeeViewModel : Input
    {
        [Display(Name ="Staff Number")]
        public string No { get; set; }
        public int Employee_Act_Qty { get; set; }
        public int Employee_Arc_Qty { get; set; }
        public int Employee_Qty { get; set; }

        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Display(Name = "Middle Name")]
        public string Middle_Name { get; set; }
        public string Known_As { get; set; }
        public string Initials { get; set; }

        [Display(Name = "National ID")]
        public string ID_Number { get; set; }

        [Display(Name = "Passport")]
        public string Passport_Number { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        public DateTime? Last_Date_Modified { get; set; }

        [Display(Name = "Responsibility Center")]
        public string Responsibility_Center { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        public DateTime? Status_Change_Date { get; set; }
        public string User_ID { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Marital Status")]
        public string Marital_Status { get; set; }
        public string Religion { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        public string County_Of_Origin { get; set; }

        [Display(Name = "County")]
        public string County_Name { get; set; }
        public string Ethnicity { get; set; }

        [Display(Name = "First Language Read/Write/Speak")]
        public string First_Language_R_W_S { get; set; }

        [Display(Name = "First Language Read")]
        public bool First_Language_Read { get; set; }

        [Display(Name = "First Language Write")]
        public bool First_Language_Write { get; set; }

        [Display(Name = "First Language Speak")]
        public bool First_Language_Speak { get; set; }

        [Display(Name = "First Language Proficiency")]
        public string First_Language_Proficiency { get; set; }

        [Display(Name = "Second Language Read/Write/Speak")]
        public string Second_Language_R_W_S { get; set; }

        [Display(Name = "Second Language Read")]
        public bool Second_Language_Read { get; set; }

        [Display(Name = "Second Language Write")]
        public bool Second_Language_Write { get; set; }

        [Display(Name = "Second Language Speak")]
        public bool Second_Language_Speak { get; set; }

        [Display(Name = "Second Language Proficiency")]
        public string Second_Language_Proficiency { get; set; }

        [Display(Name = "Additional Language")]
        public string Additional_Language { get; set; }

        [Display(Name = "Driving Licence")]
        public string Driving_Licence { get; set; }

        [Display(Name = "Disability")]
        public string Disability { get; set; }

        [Display(Name = "Disability Details")]
        public string Disability_Details { get; set; }
        public string PWD_No { get; set; }
        public bool Medical_Assessment { get; set; }
        public DateTime? Health_Assesment_Date { get; set; }
        public string Professional_Body { get; set; }

        [Display(Name = "Date of Birth")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? Date_Of_Birth { get; set; }

        [Display(Name = "Age")]
        public string DAge { get; set; }

        [Display(Name = "Employment Date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? Date_Of_Join { get; set; }

        [Display(Name = "Employee Type")]
        public string Employee_Type { get; set; }

        [Display(Name = "Length of Service")]
        public string DService { get; set; }

        [Display(Name = "Probation Start date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? Probation_Start_date { get; set; }

        [Display(Name = "Probation End date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? End_Of_Probation_Date { get; set; }

        [Display(Name = "Notice Start date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? Notice_Period_Date { get; set; }

        [Display(Name = "Contract Start date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? Contract_Start_Date { get; set; }

        [Display(Name = "Contract End date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? Contract_End_Date { get; set; }
        public DateTime? Pension_Scheme_Join { get; set; }
        public string DPension { get; set; }

        [Display(Name = "Retirement date")]
        [UIHint("Text")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public string Retirement_date { get; set; }
        public int Days_to_Retirement { get; set; }
        public string Medical_Scheme_Join { get; set; }
        public DateTime? Date_Of_Present_Appointment { get; set; }
        public string DMedical { get; set; }

        [Display(Name = "Home Phone Number")]
        public string Home_Phone_Number { get; set; }

        [Display(Name = "Cell Phone Number")]
        public string Cellular_Phone_Number { get; set; }
        public string Fax_Number { get; set; }

        [Display(Name = "Work Phone Number")]
        public string Work_Phone_Number { get; set; }

        [Display(Name = "Postal Address")]
        public string Postal_Address { get; set; }

        [Display(Name = "Postal Code")]
        public string Post_Code { get; set; }
        public string Post_Office_No { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Physical Residential Address")]
        public string Physical_Residential_Address { get; set; }

        [Display(Name = "Extension")]
        public string Ext { get; set; }

        [Display(Name = "Personal E-Mail")]
        [EmailAddress]
        public string E_Mail { get; set; }

        [Display(Name = "Official E-Mail")]
        [EmailAddress]
        public string Company_E_Mail { get; set; }

        [Display(Name = "Job")]
        public string Job_ID { get; set; }

        [Display(Name = "Acting Position")]
        public string Acting_Position { get; set; }

        [Display(Name = "Job Title")]
        public string Job_Title { get; set; }
        public string Salary_Grade { get; set; }
        public string Salary_Notch_Step { get; set; }
        public string Dimension_1_Code { get; set; }

        [Display(Name = "Department")]
        public string Dimension_1_Description { get; set; }
        public string Dimension_2_Code { get; set; }

        [Display(Name = "Division")]
        public string Dimension_2_Description { get; set; }

        [Display(Name = "Employment Type")]
        public string Employment_Type { get; set; }
        public string Send_Alert_to { get; set; }
        public bool Pays_NSSF { get; set; }
        public bool Pays_PAYE { get; set; }
        public bool Pays_NHIF { get; set; }
        public string Citizenship { get; set; }

        [Display(Name = "NHIF No")]
        public string NHIF_No { get; set; }

        [Display(Name = "NSSF No")]
        public string NSSF_No { get; set; }

        [Display(Name = "HELB No")]
        public string HELB_No { get; set; }

        [Display(Name = "PIN")]
        public string PAYE_Number { get; set; }
        public string Main_Bank { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        public string Branch_Bank { get; set; }

        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "Bank Account")]
        public string Bank_Account_Number { get; set; }

        [Display(Name = "Payment Mode")]
        public string Payment_Mode { get; set; }
        public string Posting_Group { get; set; }
        public decimal Basic_Pay { get; set; }
        public bool Temporary_Employee { get; set; }
        public string Temporary_Employee_Rate { get; set; }
        public decimal No_Of_Days_Worked { get; set; }
        public decimal Reimbursed_Leave_Days { get; set; }
        public decimal Allocated_Leave_Days { get; set; }
        public decimal Total_Leave_Days { get; set; }
        public decimal Total_Leave_Taken { get; set; }
        public decimal Leave_Balance { get; set; }
        public decimal davailable { get; set; }
        public string Leave_Family { get; set; }
        public decimal Served_Notice_Period { get; set; }
        public string Date_Of_Leaving { get; set; }
        public string Termination_Category { get; set; }
        public string Exit_Interview_Date { get; set; }
        public string Exit_Interview_Done_by { get; set; }
        public string Allow_Re_Employment_In_Future { get; set; }
        public string Date_Filter { get; set; }

        //public IEnumerable<SelectListItem> DepartmentList
        //{
        //    get
        //    {
        //        var list = DropDownLists.PopulateList("dimension", "DEPARTMENT");
        //        return list;
        //    }
        //}
        //public IEnumerable<SelectListItem> CountyList
        //{
        //    get
        //    {
        //        var list = DropDownLists.PopulateList("dimension", "COUNTY");
        //        return list;
        //    }
        //}
        //public IEnumerable<SelectListItem> StationList
        //{
        //    get
        //    {
        //        var list = DropDownLists.PopulateList("dimension", "STATION");
        //        return list;
        //    }
        //}
        //public IEnumerable<SelectListItem> SubStations
        //{
        //    get
        //    {
        //        var list = DropDownLists.PopulateList("dimension", "SUB STATION");
        //        return list;
        //    }
        //}


    }
}