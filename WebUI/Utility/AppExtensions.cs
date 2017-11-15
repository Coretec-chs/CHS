using System;
using System.Web;

using Elmah;
using System.Security.Principal;
using System.Security.Claims;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebUI.Utility
{
    public static class AppExtensions
    {
        public static void Log(this Exception ex)
        {
            if (HttpContext.Current == null)
                ErrorLog.GetDefault(null).Log(new Error(ex));
            else
                ErrorSignal.FromCurrentContext().Raise(ex);
        }


        public static string GetUserRecId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Sid);
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : null;
        }

        //public static string GetUserName(this IIdentity identity)
        //{
        //    return ((ClaimsIdentity)identity).GetUserName();
        //}

        public static string GetVendorName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.GivenName);
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : null;
        }

        public static string GetVendorPIN(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.PrimarySid);
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : null;
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}