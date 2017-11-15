using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls; //For SortBy method
using System.Web.Mvc;

namespace WebUI.Utility
{
    public class ResultSet
    {
        //public List<Loanee> LoaneeFilterResult(out int totalRecords, string search, string sortOrder, int start, int length, IEnumerable<Loanee> dtResult)
        //{ 
        //    IQueryable<Loanee> query = dtResult.AsQueryable();           

        //    if (!String.IsNullOrWhiteSpace(search))
        //    {
        //        query = query.Where(p => (p.NationalIDNo.ToLower() + " " + p.LoaneeName.ToLower() + " " + p.StaffNo.ToLower() + " " + p.strAmount.ToLower()).Contains(search.ToLower()));
        //    }
            
        //    var queryList = query.ToList().AsQueryable();
            
        //    totalRecords = queryList.Count();

        //    if (!String.IsNullOrWhiteSpace(sortOrder))
        //    {
        //        queryList = queryList.SortBy(sortOrder).Skip(start).Take(length);
        //    }
        //    else
        //    {
        //        queryList = queryList.Skip(start).Take(length);
        //    }
        //    return queryList.ToList();
        //}
    }
}