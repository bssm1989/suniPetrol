using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using santisart_app.Models;

namespace santisart_app.Controllers
{
    public class EnrollEmpCousesController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: EnrollEmpCouses
        public ActionResult Index()
        {
            var enrollEmpCouse = db.EnrollEmpCouse.Include(e => e.Employee).Include(e => e.EnrollCouse).Include(e => e.EnrollYearSemester);
            return View(enrollEmpCouse.ToList());
        }

        // GET: EnrollEmpCouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            if (enrollEmpCouse == null)
            {
                return HttpNotFound();
            }
            return View(enrollEmpCouse);
        }

        // GET: EnrollEmpCouses/Create
        public ActionResult Create()
        {
            List<object> newListEmp = new List<object>();
            foreach (var member in db.Employee)
                newListEmp.Add(new
                {
                    Id = member.EmpId,
                    Name = member.EmpName + " " + member.EmpLname
                });
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester.Include(x=>x.YearEdu))
                newListYear.Add(new
                {
                    Id = year.EnrollYearSemesterId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester
                });
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse)
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name =  Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
            ViewBag.EnCouseId = new SelectList(newListCouse, "Id", "Name");
            ViewBag.EnEmpId = new SelectList(newListEmp, "Id", "Name");
            ViewBag.EnYearSemId = new SelectList(newListYear, "Id", "Name");
            return View();
        }

        // POST: EnrollEmpCouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnCouseId,EnEmpCosTimestamp,EnrollEmpCouseId,EnEmpId,EnYearSemId")] EnrollEmpCouse enrollEmpCouse)
        {
            enrollEmpCouse.EnEmpCosTimestamp = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.EnrollEmpCouse.Add(enrollEmpCouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnEmpId =       new SelectList(db.Employee, "EmpId", "EmpTitle", enrollEmpCouse.EnEmpId);
            ViewBag.EnCouseId =     new SelectList(db.EnrollCouse, "EnrollCouseID", "CouseTxtId", enrollEmpCouse.EnCouseId);
            ViewBag.EnYearSemId =   new SelectList(db.EnrollYearSemester, "EnrollYearSemesterId", "EnrollYearSemesterId", enrollEmpCouse.EnYearSemId);
            return View(enrollEmpCouse);
        }

        // GET: EnrollEmpCouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            if (enrollEmpCouse == null)
            {
                return HttpNotFound();
            }
            List<object> newListEmp = new List<object>();
            foreach (var member in db.Employee)
                newListEmp.Add(new
                {
                    Id = member.EmpId,
                    Name = member.EmpName + " " + member.EmpLname
                });
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester.Include(x => x.YearEdu))
                newListYear.Add(new
                {
                    Id = year.EnrollYearSemesterId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester
                });
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse)
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name =  Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
            ViewBag.EnCouseId = new SelectList(newListCouse, "Id", "Name",enrollEmpCouse.EnCouseId);
            ViewBag.EnEmpId = new SelectList(newListEmp, "Id", "Name", enrollEmpCouse.EnEmpId);
            ViewBag.EnYearSemId = new SelectList(newListYear, "Id", "Name", enrollEmpCouse.EnYearSemId);
            return View(enrollEmpCouse);
        }

        // POST: EnrollEmpCouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnCouseId,EnEmpCosTimestamp,EnrollEmpCouseId,EnEmpId,EnYearSemId")] EnrollEmpCouse enrollEmpCouse)
        {
            enrollEmpCouse.EnEmpCosTimestamp = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(enrollEmpCouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<object> newListEmp = new List<object>();
            foreach (var member in db.Employee)
                newListEmp.Add(new
                {
                    Id = member.EmpId,
                    Name = member.EmpName + " " + member.EmpLname
                });
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester.Include(x => x.YearEdu))
                newListYear.Add(new
                {
                    Id = year.EnrollYearSemesterId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester
                });
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse)
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name =  Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
            ViewBag.EnCouseId = new SelectList(newListCouse, "Id", "Name", enrollEmpCouse.EnCouseId);
            ViewBag.EnEmpId = new SelectList(newListEmp, "Id", "Name", enrollEmpCouse.EnEmpId);
            ViewBag.EnYearSemId = new SelectList(newListYear, "Id", "Name", enrollEmpCouse.EnYearSemId);
            return View(enrollEmpCouse);
        }

        // GET: EnrollEmpCouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            if (enrollEmpCouse == null)
            {
                return HttpNotFound();
            }
            return View(enrollEmpCouse);
        }

        // POST: EnrollEmpCouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            db.EnrollEmpCouse.Remove(enrollEmpCouse);
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
