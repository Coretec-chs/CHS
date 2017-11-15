
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebUI.Utility
{
    public static class DropDownLists
    {
        public static IEnumerable<SelectListItem> PopulateList(string listType, params object[] filterValues)
        {
            IEnumerable<SelectListItem> droplist = null;
            var repo = new NavConnector().GetODataService();

            switch (listType)
            {
                case "dimension":
                    if (filterValues.Length == 1)
                    {
                        var dimCode = filterValues[0].ToString();
                        droplist = repo.Dimensions.Where(m => m.Dimension_Code == dimCode).ToList()
                            .OrderBy(o => o.Name)
                            .Select(m => new SelectListItem { Text = m.Name, Value = m.Code });
                    }
                    else
                    {
                        var dimCode = filterValues[0].ToString();
                        var donor = filterValues[1].ToString();
                        droplist = repo.Dimensions.Where(m => m.Dimension_Code == dimCode  && m.Donor_Code == donor).ToList()
                            .OrderBy(o => o.Name)
                            .Select(m => new SelectListItem { Text = m.Name, Value = m.Code });
                    }

                    break;
                case "employee":
                    droplist = repo.HREmployee.Where(m => m.Status == "Active").ToList()
                        .OrderBy(o => o.First_Name)
                        .Select(m => new SelectListItem { Text = string.Concat(m.First_Name, " ", m.Last_Name), Value = m.No });

                    break;
                case "leavetype":
                    var gender = filterValues[0].ToString();
                        droplist = repo.HRLeaveTypes.Where(m => m.Gender == gender || m.Gender == "Both").ToList()
                            .OrderBy(o => o.Description)
                            .Select(m => new SelectListItem { Text = m.Description, Value = m.Code });
                    break;
                case "currency":
                    droplist = repo.Currencies.ToList()
                        .OrderBy(o => o.Code)
                        .Select(m => new SelectListItem { Text = m.Code, Value = m.Code });
                    break;
                case "advanceType":
                    droplist = repo.ReceiptandPaymentTypesList.Where(m=>m.Type == "Imprest").ToList()
                        .OrderBy(o => o.Description)
                        .Select(m => new SelectListItem { Text = m.Description, Value = m.Code });
                    break;
                case "destinationCode":
                    droplist = repo.DestinationCodeList.ToList()
                        .OrderBy(o => o.Destination_Name)
                        .Select(m => new SelectListItem { Text = m.Destination_Name, Value = m.Destination_Code });
                    break;
            }
            return droplist;
        }

        public static IEnumerable<SelectListItem> PopulateListAx(string listType, params object[] filterValues)
        {
            IEnumerable<SelectListItem> droplist = null;
            //object filval = null;
            //using (var a = new Ax())
            //{
            //    switch (listType)
            //    {
            //        case "educationlevel":
            //            droplist = d.company.AsNoTracking().OrderBy(o => o.description)
            //                .Select(m => new SelectListItem { Text = m.description.ToUpper(), Value = m.id.ToString() }).ToList();
            //            break;
            //    }
            //}
            return droplist;
        }
        //public static IEnumerable<SearchItem> SearchItemList(string listType, params object[] filterValues)
        //{
        //    IEnumerable<SearchItem> searchlist = null;
        //    //object filval = null;
        //    //using (var d = new Db())
        //    //{
        //    //    //switch (listType)
        //    //    //{

        //    //    //}
        //    //}
        //    return searchlist;
        //}
    }
}