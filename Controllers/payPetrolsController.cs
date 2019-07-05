using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using santisart_app.Models;

namespace santisart_app.Controllers
{
    public class payPetrolsController : Controller
    {
        private suniPetrolEntities db = new suniPetrolEntities();

        // GET: payPetrols
        public ActionResult Index()
        {
            var payPetrols = db.payPetrols.ToList();
            return View(payPetrols);
        }
        public ActionResult FindExcel()
        {
            return View();
        }
        public ActionResult ReportMonthlyKpi()
        {
           List<int> listCarid = db.trucks.Where(x => x.company_id == 2 ).OrderBy(x=>x.excelPageReportKpi).Select(x=>x.car_id).ToList();
            var arrayListCarid = db.trucks.Where(x => x.company_id == 2)
                .Select(y =>new { y.car_id, y.excelPageReportKpi })
                .OrderBy(x=>x.excelPageReportKpi)
                .ThenBy(x=>x.car_id).ToList();
            //List<int> listCaridPageExcel = db.trucks.Where(x => x.company_id == 2).Select(x => x.excelPageReportKpi).ToList();
            List<payPetrol> listLastMeter = new List<payPetrol>();
             var payPetrols = db.payPetrols.Include(p => p.truck)
                .Where(x=>x.truck.company.id==2 && x.mem_date.Value.Month==DateTime.Now.Month );
            foreach (var item in listCarid)
            {
                var listMeter = db.payPetrols.Where(x => x.car_id == item && 
                x.mem_date.Value.Ticks < DateTime.Parse(DateTime.Now.Month.ToString()+"/1/"+ DateTime.Now.Year.ToString()+" 00:00:00").Ticks)
                .OrderByDescending(x => x.mem_date).FirstOrDefault();
                if (listMeter != null)
                {

                    listLastMeter.Add(listMeter);
                }
            }
            ViewData["listTruck"] = listCarid;
            ViewData["listMeterLast"] = listLastMeter;
    
            var application = new Microsoft.Office.Interop.Excel.Application();
            string pathExcel = Server.MapPath("ExcelTemp\\KPI-STC-themp.xlsx");// + V;
                                                                       // string fileName = application.StartupPath + @"\ExcelTemp\Test.xlsm";//C:\Users\mumee\source\repos\santisart_app\santisart_app\ExcelTemp\Test.xlsx "\\ExcelTemp\\Test.xlsx";
            var workbook = application.Workbooks.Open(pathExcel);
            String datetiemText = DateTime.Now.ToString(" dd-MM-yyyy hh-mm-ss");
            workbook.SaveCopyAs(Server.MapPath("ExcelTemp\\KPI-STC"+ datetiemText+".xlsx"));
            //var workbookToEdit=workbook.cop
            workbook.Close();
            workbook = application.Workbooks.Open(Server.MapPath("ExcelTemp\\KPI-STC" + datetiemText + ".xlsx"));
                var indexMeter = 0;

            //worksheet.Copy(Type.Missing, worksheet);
            //worksheet = workbook.Worksheets[2] as Microsoft.Office.Interop.Excel.Worksheet;
            //foreach (var item in listLastMeter.Select(x=>x.meter_truck))
            //{
            //    indexMeter++;
            //    worksheet.Cells[indexMeter,1 ] = item;
            //}
         
            var payPetrol = payPetrols.OrderBy(x => x.truck.excelPageReportKpi).ThenBy(x => x.mem_date);
            
            //foreach (var item in arrayListCarid)
            //{
            //    worksheet.Cells[indexRow, indexCol * 5 + 1] = item.car_id;
            //    worksheet.Cells[indexRow, indexCol * 5 +2] = item.excelPageReportKpi;
            //    indexRow++;
            //}
            var worksheet = workbook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet; 
            var pageExcel = 0;
            //read from cell
            for (int ws = 1; ws < workbook.Sheets.Count; ws++)
            {
                int[] caridList = new int[20];
                int indexCol = 0, indexRow = 5;
                worksheet = workbook.Worksheets[ws] as Microsoft.Office.Interop.Excel.Worksheet;


                int lengthRow = 10, idCarTxtRow = 1;
                for (int i = 1; i < lengthRow; i++)
                {
                    var cellValue = worksheet.Cells[i, 1].Value ?? "";
                    if (cellValue == "ทะเบียนรถ")
                    {
                        idCarTxtRow = i;
                        break;
                    }
                }
                string cellCaridTxt;
                List<string> listCaridExcel = new List<string>();
                for (int i = 1; i < 80; i++)
                {
                    var cellValue = worksheet.Cells[idCarTxtRow, i].Value ?? "";
                    if (cellValue == "ทะเบียนรถ")
                    {
                        cellCaridTxt = worksheet.Cells[idCarTxtRow, i + 1].Value ?? "";
                        cellCaridTxt = (cellCaridTxt.Split(' '))[0];
                        //if(cellCaridTxt!="")
                        listCaridExcel.Add(cellCaridTxt.ToString());
                    }

                }
                foreach (var item in listCaridExcel)
                {

                    indexRow = 6;
                    var idCar = db.trucks.Where(x => x.id_truck == item).Select(x => x.car_id).FirstOrDefault();
                    if (idCar != 0)
                    {

                        foreach (var items in payPetrol.Where(x => x.car_id == idCar))
                        {


                            if (indexRow == 6)
                            {
                                worksheet.Cells[5, indexCol * 5 + 2] = listLastMeter.Where(x => x.car_id == idCar).Select(x => x.meter_truck).FirstOrDefault();
                                worksheet.Cells[5, indexCol * 5 + 4] = listLastMeter.Where(x => x.car_id == idCar).Select(x => x.pay_petrol).FirstOrDefault();
                                worksheet.Cells[indexRow, indexCol * 5 + 1] = items.mem_date.Value.ToString("dd/MM/yyyy");
                                worksheet.Cells[indexRow, indexCol * 5 + 2] = items.meter_truck;
                                worksheet.Cells[indexRow, indexCol * 5 + 3].Formula = "=" + GetCellAddress(indexRow, indexCol * 5 + 2) + "-" + GetCellAddress(indexRow - 1, indexCol * 5 + 2);
                                worksheet.Cells[indexRow, indexCol * 5 + 4] = items.pay_petrol;
                            }
                            else
                            {
                                worksheet.Cells[indexRow, indexCol * 5 + 1] = items.mem_date.Value.ToString("dd/MM/yyyy");
                                worksheet.Cells[indexRow, indexCol * 5 + 2] = items.meter_truck;
                                worksheet.Cells[indexRow, indexCol * 5 + 3].Formula = "=" + GetCellAddress(indexRow, indexCol * 5 + 2) + "-" + GetCellAddress(indexRow - 1, indexCol * 5 + 2);
                                worksheet.Cells[indexRow, indexCol * 5 + 4] = items.pay_petrol;
                            }
                            indexRow++;
                        }
                    }
                    indexCol++;
                }
            }
            //foreach (var item in arrayListCarid)
            //{
            //    if (item.excelPageReportKpi == null && pageExcel != 999)
            //    {
            //        worksheet = workbook.Worksheets[4] as Microsoft.Office.Interop.Excel.Worksheet;
            //        pageExcel = 999;

            //        indexCol = 0;
            //    }
            //    else if (pageExcel != item.excelPageReportKpi && item.excelPageReportKpi != null)
            //    {
            //        pageExcel = item.excelPageReportKpi ?? 0;

            //        worksheet = workbook.Worksheets[pageExcel] as Microsoft.Office.Interop.Excel.Worksheet;

            //        indexCol = 0;

            //    }
            //    indexRow = 6;
            //    foreach (var items in payPetrol.Where(x => x.car_id == item.car_id))
            //    {


            //        if (indexRow == 6)
            //        {
            //            worksheet.Cells[5, indexCol * 5 + 2] = listLastMeter.Where(x => x.car_id == item.car_id).Select(x => x.meter_truck).FirstOrDefault();
            //            worksheet.Cells[5, indexCol * 5 + 4] = listLastMeter.Where(x => x.car_id == item.car_id).Select(x => x.pay_petrol).FirstOrDefault();
            //            worksheet.Cells[indexRow, indexCol * 5 + 1] = items.mem_date.Value.ToString("dd/MM/yyyy");
            //            worksheet.Cells[indexRow, indexCol * 5 + 2] = items.meter_truck;
            //            worksheet.Cells[indexRow, indexCol * 5 + 3].Formula = "=" + GetCellAddress(indexRow, indexCol * 5 + 2) + "-" + GetCellAddress(indexRow - 1, indexCol * 5 + 2);
            //            worksheet.Cells[indexRow, indexCol * 5 + 4] = items.pay_petrol;
            //        }
            //        else
            //        {
            //            worksheet.Cells[indexRow, indexCol * 5 + 1] = items.mem_date.Value.ToString("dd/MM/yyyy");
            //            worksheet.Cells[indexRow, indexCol * 5 + 2] = items.meter_truck;
            //            worksheet.Cells[indexRow, indexCol * 5 + 3].Formula = "=" + GetCellAddress(indexRow, indexCol * 5 + 2) + "-" + GetCellAddress(indexRow - 1, indexCol * 5 + 2);
            //            worksheet.Cells[indexRow, indexCol * 5 + 4] = items.pay_petrol;
            //        }
            //        indexRow++;
            //    }
            //    indexCol++;
            //}





            //// Save.
            workbook.Save();
            workbook.Close();
            return View(payPetrols.ToList());
        }
        [HttpPost]
        public ActionResult ReportMonthlyKpiExcel(int dropdownMonth)
        {
           
            List<string> listCompny =new List<string> { "SST","STS" ,"STC"};
            foreach (var company in listCompny)
            {
                List<int> listCarid = db.trucks.Where(x => x.company.Name == company).OrderBy(x => x.excelPageReportKpi).Select(x => x.car_id).ToList();

                List<payPetrol> listLastMeter = new List<payPetrol>();
                var payPetrols = db.payPetrols.Include(p => p.truck)
                    .Where(x => x.truck.company.Name ==company  && x.mem_date.Value.Month == dropdownMonth).ToList();
                var firstMonth = DateTime.Parse(dropdownMonth.ToString() + "/1/" + DateTime.Now.Year.ToString()+" 00:00:00");

                foreach (var item in listCarid)
                {
                    var listMeter = db.payPetrols.Where(x => x.car_id == item &&
                DateTime.Compare(x.mem_date.Value, firstMonth)<0)
               .OrderByDescending(x => x.mem_date).FirstOrDefault();
                    if (listMeter != null)
                    {

                        listLastMeter.Add(listMeter);
                    }
                }
                var application = new Microsoft.Office.Interop.Excel.Application();
                string pathExcel = Server.MapPath("ExcelTemp\\KPI-"+company+"-themp.xlsx"); var workbook = application.Workbooks.Open(pathExcel);
                String datetiemText = DateTime.Now.ToString(" dd-MM-yyyy hh-mm-ss");
                workbook.SaveCopyAs(Server.MapPath("ExcelTemp\\KPI-" + company  + datetiemText + ".xlsx"));
                //var workbookToEdit=workbook.cop
                workbook.Close();
                workbook = application.Workbooks.Open(Server.MapPath("ExcelTemp\\KPI-"+company + datetiemText + ".xlsx"));
         



                var payPetrol = payPetrols.OrderBy(x => x.truck.excelPageReportKpi).ThenBy(x => x.mem_date);


                var worksheet = workbook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
                var pageExcel = 0;
                //read from cell
                for (int ws = 1; ws < workbook.Sheets.Count; ws++)
                {
                    int[] caridList = new int[20];
                    int indexCol = 0, indexRow = 5;
                    worksheet = workbook.Worksheets[ws] as Microsoft.Office.Interop.Excel.Worksheet;


                    int lengthRow = 10, idCarTxtRow = 1;
                    for (int i = 1; i < lengthRow; i++)
                    {
                        var cellValue = worksheet.Cells[i, 1].Value ?? "";
                        if (cellValue == "ทะเบียนรถ")
                        {
                            idCarTxtRow = i;
                            break;
                        }
                    }
                    string cellCaridTxt;
                    List<string> listCaridExcel = new List<string>();
                    for (int i = 1; i < 80; i++)
                    {
                        var cellValue = worksheet.Cells[idCarTxtRow, i].Value ?? "";
                        if (cellValue == "ทะเบียนรถ")
                        {
                            cellCaridTxt = worksheet.Cells[idCarTxtRow, i + 1].Value ?? "";
                            cellCaridTxt = (cellCaridTxt.Split(' '))[0];
                            //if(cellCaridTxt!="")
                            listCaridExcel.Add(cellCaridTxt.ToString());
                        }

                    }
                    foreach (var item in listCaridExcel)
                    {

                        indexRow = 6;
                        var idCar = db.trucks.Where(x => x.id_truck == item).Select(x => x.car_id).FirstOrDefault();
                        if (idCar != 0)
                        {

                            foreach (var items in payPetrol.Where(x => x.car_id == idCar))
                            {


                                if (indexRow == 6)
                                {
                                    worksheet.Cells[5, indexCol * 5 + 2] = listLastMeter.Where(x => x.car_id == idCar).Select(x => x.meter_truck).FirstOrDefault();
                                    worksheet.Cells[5, indexCol * 5 + 4] = listLastMeter.Where(x => x.car_id == idCar).Select(x => x.pay_petrol).FirstOrDefault();
                                    worksheet.Cells[indexRow, indexCol * 5 + 1] = items.mem_date.Value.ToString("dd/MM/yyyy");
                                    worksheet.Cells[indexRow, indexCol * 5 + 2] = items.meter_truck;
                                    worksheet.Cells[indexRow, indexCol * 5 + 3].Formula = "=" + GetCellAddress(indexRow, indexCol * 5 + 2) + "-" + GetCellAddress(indexRow - 1, indexCol * 5 + 2);
                                    worksheet.Cells[indexRow, indexCol * 5 + 4] = items.pay_petrol;
                                }
                                else
                                {
                                    worksheet.Cells[indexRow, indexCol * 5 + 1] = items.mem_date.Value.ToString("dd/MM/yyyy");
                                    worksheet.Cells[indexRow, indexCol * 5 + 2] = items.meter_truck;
                                    worksheet.Cells[indexRow, indexCol * 5 + 3].Formula = "=" + GetCellAddress(indexRow, indexCol * 5 + 2) + "-" + GetCellAddress(indexRow - 1, indexCol * 5 + 2);
                                    worksheet.Cells[indexRow, indexCol * 5 + 4] = items.pay_petrol;
                                }
                                indexRow++;
                            }
                        }
                        indexCol++;
                    }
                }

                //// Save.
                workbook.Save();
                workbook.Close();
            }
            
            return View();
        }
        static string GetCellAddress(int row, int col)
        {
            StringBuilder sb = new StringBuilder();

            do
            {
                col--;
                sb.Insert(0, (char)('A' + (col % 26)));
                col /= 26;
            } while (col > 0);
            sb.Append(row);
            return sb.ToString();
        }
        // GET: payPetrols/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payPetrol payPetrol = db.payPetrols.Find(id);
            if (payPetrol == null)
            {
                return HttpNotFound();
            }
            return View(payPetrol);
        }

        // GET: payPetrols/Create
        public ActionResult Create()
        {
            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck");
            return View();
        }

        // POST: payPetrols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "system_petrol,meter_truck,pay_petrol,car_id,mem_date,timestamp,id")] payPetrol payPetrol)
        {
            if (ModelState.IsValid)
            {
                db.payPetrols.Add(payPetrol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck", payPetrol.car_id);
            return View(payPetrol);
        }

        // GET: payPetrols/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payPetrol payPetrol = db.payPetrols.Find(id);
            if (payPetrol == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck", payPetrol.car_id);
            return View(payPetrol);
        }

        // POST: payPetrols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "system_petrol,meter_truck,pay_petrol,car_id,mem_date,timestamp,id")] payPetrol payPetrol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payPetrol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck", payPetrol.car_id);
            return View(payPetrol);
        }

        // GET: payPetrols/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payPetrol payPetrol = db.payPetrols.Find(id);
            if (payPetrol == null)
            {
                return HttpNotFound();
            }
            return View(payPetrol);
        }

        // POST: payPetrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payPetrol payPetrol = db.payPetrols.Find(id);
            db.payPetrols.Remove(payPetrol);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
