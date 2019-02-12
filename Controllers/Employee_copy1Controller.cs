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
    public class Employee_copy1Controller : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: Employee_copy1
        public ActionResult Index()
        {
            return View(db.Employee_copy1.ToList());
        }

        // GET: Employee_copy1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_copy1 employee_copy1 = db.Employee_copy1.Find(id);
            if (employee_copy1 == null)
            {
                return HttpNotFound();
            }
            return View(employee_copy1);
        }

        // GET: Employee_copy1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee_copy1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpTitle,EmpName,EmpLname,EmpBirthday,EmpIdcard,EmpPsis_id,EmpStatus,EmpTimestamp,EmpTelephone,Empbank_id,EmpId,EmpGender")] Employee_copy1 employee_copy1)
        {
            if (ModelState.IsValid)
            {
                db.Employee_copy1.Add(employee_copy1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee_copy1);
        }

        // GET: Employee_copy1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_copy1 employee_copy1 = db.Employee_copy1.Find(id);
            if (employee_copy1 == null)
            {
                return HttpNotFound();
            }
            return View(employee_copy1);
        }

        // POST: Employee_copy1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpTitle,EmpName,EmpLname,EmpBirthday,EmpIdcard,EmpPsis_id,EmpStatus,EmpTimestamp,EmpTelephone,Empbank_id,EmpId,EmpGender")] Employee_copy1 employee_copy1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee_copy1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee_copy1);
        }

        // GET: Employee_copy1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_copy1 employee_copy1 = db.Employee_copy1.Find(id);
            if (employee_copy1 == null)
            {
                return HttpNotFound();
            }
            return View(employee_copy1);
        }

        // POST: Employee_copy1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee_copy1 employee_copy1 = db.Employee_copy1.Find(id);
            db.Employee_copy1.Remove(employee_copy1);
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
