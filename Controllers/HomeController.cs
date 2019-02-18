using System.IO;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Office.Interop.Excel;
using System.Web;

namespace santisart_app.Controllers
{
    public class HomeController : Controller
    {
        private const string V = @"ExcelTemp\Book5.xlsx";

        public ActionResult Index()
        {

            // string topLeft = "A1";
            // string bottomRight = "A4";
            // string graphTitle = "Graph Title";
            // string xAxis = "Time";
            // string yAxis = "Value";
            // var application = new Microsoft.Office.Interop.Excel.Application();
            // string pathExcel = Server.MapPath("ExcelTemp\\Book5.xlsx");// + V;
            //// string fileName = application.StartupPath + @"\ExcelTemp\Test.xlsm";//C:\Users\mumee\source\repos\santisart_app\santisart_app\ExcelTemp\Test.xlsx "\\ExcelTemp\\Test.xlsx";
            // var workbook = application.Workbooks.Open(pathExcel);
            // var worksheet = workbook.Worksheets[1] as
            //     Microsoft.Office.Interop.Excel.Worksheet;

            // // Add chart.
            // var charts = worksheet.ChartObjects() as
            //     Microsoft.Office.Interop.Excel.ChartObjects;
            // var chartObject = charts.Add(60, 10, 300, 300) as
            //     Microsoft.Office.Interop.Excel.ChartObject;
            // var chart = chartObject.Chart;

            // // Set chart range.
            // var range = worksheet.get_Range(topLeft, bottomRight);
            // chart.SetSourceData(range);

            // // Set chart properties.
            // chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
            // chart.ChartWizard(Source: range,
            //     Title: graphTitle,
            //     CategoryTitle: xAxis,
            //     ValueTitle: yAxis);

            // //// Save.
            // workbook.Save();
            // workbook.Close();
            FileStream fs1 = new FileStream(Server.MapPath("ExcelTemp\\test.txt"), FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(Server.MapPath("ExcelTemp\\test.txt"));
            writer.Close();
            return View();
        }

        public ActionResult AnotherLink()
        {
           
            
            return View("Index");
        }
    }
}
