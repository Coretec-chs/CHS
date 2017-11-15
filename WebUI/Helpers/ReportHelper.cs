using System;
using System.Xml;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace WebUI
{
    public static class ReportHelper
    {
        private static string GetDeviceInfo(string format = "pdf", string marginscsv = "0,0,0,0", string orientation = "portrait", string size = "827,1129")
        {
            string deviceInfo;
            string[] margins = marginscsv.Split(',');
            string[] sizes = size.Split(',');

            string tm = margins[0]; // Top margin
            string lm = margins[1]; // Left margin
            string bm = margins[2]; // Bottom margin
            string rm = margins[3]; // Right margin
            string pw = (Convert.ToDouble(sizes[0]) / 100).ToString() + "in";
            string ph = (Convert.ToDouble(sizes[1]) / 100).ToString() + "in";

            deviceInfo = (orientation == "portrait") ? "<PageWidth>" + pw + "</PageWidth><PageHeight>" + ph + "</PageHeight>" : "<PageWidth>" + ph + "</PageWidth><PageHeight>" + pw + "</PageHeight>";

            //deviceInfo = "<PageWidth>"+ pw +"</PageWidth><PageHeight>"+ ph +"</PageHeight>";

            deviceInfo = "<DeviceInfo>" + "<OutputFormat>" + format + "</OutputFormat>" + deviceInfo + "  <MarginTop>" + tm + "cm</MarginTop>" + "  <MarginLeft>" + lm + "cm</MarginLeft>" + "  <MarginRight>" + rm + "cm</MarginRight>" + "  <MarginBottom>" + bm + "cm</MarginBottom>" + "</DeviceInfo>";

            return deviceInfo;
        }

        public static string GetDeviceInfoFromReport(ServerReport rpt, string format, string orientation)
        {
            string strMargins;
            string size;

            //Render overwrites margins defined in RDLC; capture margins in RDLC
            ReportPageSettings pageSettings = rpt.GetDefaultPageSettings();
            //strMargins = String.Concat((Convert.ToDouble(pageSettings.Margins.Top) / 40.0).ToString(), ",", (Convert.ToDouble(pageSettings.Margins.Left) / 40.0).ToString(), ",", (Convert.ToDouble(pageSettings.Margins.Bottom) / 40.0).ToString(), ",", (Convert.ToDouble(pageSettings.Margins.Right) / 40.0).ToString());
            strMargins = String.Concat((Convert.ToDouble(pageSettings.Margins.Top) / 100.0).ToString(), ",", (Convert.ToDouble(pageSettings.Margins.Left) / 100.0).ToString(), ",", (Convert.ToDouble(pageSettings.Margins.Bottom) / 100.0).ToString(), ",", (Convert.ToDouble(pageSettings.Margins.Right) / 100.0).ToString());

            //Capture report orientation
            orientation = pageSettings.IsLandscape ? "landscape" : "portrait";
            size = String.Concat(pageSettings.PaperSize.Width.ToString(), ",", pageSettings.PaperSize.Height.ToString());
            return GetDeviceInfo(format, strMargins, orientation, size);
        }

        public static StringReader FormatReportForTerritory(string reportPath, string orientation)
        {
            XmlDocument xmlDoc = new XmlDocument();
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream xmlStream;

            xmlStream = asm.GetManifestResourceStream(reportPath);

            try
            {
                xmlDoc.Load(reportPath);
            }
            catch
            {
                //Ignore??!?
            }

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("nm", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
            nsmgr.AddNamespace("rd", "http://schemas.microsoft.com/sqlserver/reporting/reportdesigner");

            StringReader rdlcOutputStream = new StringReader(xmlDoc.DocumentElement.OuterXml);

            return rdlcOutputStream;
        }

        public static StringReader LoadReportXml(string reportPath, string orientation)
        {
            XmlDocument xmlDoc = new XmlDocument();
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream xmlStream;

            xmlStream = asm.GetManifestResourceStream(reportPath);

            try
            {
                xmlDoc.Load(reportPath);
            }
            catch
            {
                //Ignore??!?
            }

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("nm", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
            nsmgr.AddNamespace("rd", "http://schemas.microsoft.com/sqlserver/reporting/reportdesigner");

            StringReader rdlcOutputStream = new StringReader(xmlDoc.DocumentElement.OuterXml);

            return rdlcOutputStream;
        }

    }

}
