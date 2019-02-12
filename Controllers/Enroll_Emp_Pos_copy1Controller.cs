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
    public class Enroll_Emp_Pos_copy1Controller : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: Enroll_Emp_Pos_copy1
        public ActionResult Index()
        {
            var enroll_Emp_Pos_copy1 = db.Enroll_Emp_Pos_copy1.Include(e => e.Employee_copy1);
            return View(enroll_Emp_Pos_copy1.ToList());
        }

        // GET: Enroll_Emp_Pos_copy1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll_Emp_Pos_copy1 enroll_Emp_Pos_copy1 = db.Enroll_Emp_Pos_copy1.Find(id);
            if (enroll_Emp_Pos_copy1 == null)
            {
                return HttpNotFound();
            }
            return View(enroll_Emp_Pos_copy1);
        }

        // GET: Enroll_Emp_Pos_copy1/Create
        public ActionResult Create()
        {
            ViewBag.Employee_id = new SelectList(db.Employee_copy1, "EmpId", "EmpTitle");
            return View();
        }

        // POST: Enroll_Emp_Pos_copy1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee_id,Position_id,EnrEmpPosStatus,EnrEmpPosTimestamp,EnrEmpPosId")] Enroll_Emp_Pos_copy1 enroll_Emp_Pos_copy1)
        {
            if (ModelState.IsValid)
            {
                db.Enroll_Emp_Pos_copy1.Add(enroll_Emp_Pos_copy1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_id = new SelectList(db.Employee_copy1, "EmpId", "EmpTitle", enroll_Emp_Pos_copy1.Employee_id);
            return View(enroll_Emp_Pos_copy1);
        }

        // GET: Enroll_Emp_Pos_copy1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll_Emp_Pos_copy1 enroll_Emp_Pos_copy1 = db.Enroll_Emp_Pos_copy1.Find(id);
            if (enroll_Emp_Pos_copy1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_id = new SelectList(db.Employee_copy1, "EmpId", "EmpTitle", enroll_Emp_Pos_copy1.Employee_id);
            return View(enroll_Emp_Pos_copy1);
        }

        // POST: Enroll_Emp_Pos_copy1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_id," +
            "Position_id,EnrEmpPosStatus,EnrEmpPosTimestamp,EnrEmpPosId")] Enroll_Emp_Pos_copy1 enroll_Emp_Pos_copy1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enroll_Emp_Pos_copy1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_id = new SelectList(db.Employee_copy1, "EmpIdcard",  "EmpId", "EmpTitle", enroll_Emp_Pos_copy1.Employee_id);
            return View(enroll_Emp_Pos_copy1);
        }

        // GET: Enroll_Emp_Pos_copy1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll_Emp_Pos_copy1 enroll_Emp_Pos_copy1 = db.Enroll_Emp_Pos_copy1.Find(id);
            if (enroll_Emp_Pos_copy1 == null)
            {
                return HttpNotFound();
            }
            return View(enroll_Emp_Pos_copy1);
        }

        // POST: Enroll_Emp_Pos_copy1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enroll_Emp_Pos_copy1 enroll_Emp_Pos_copy1 = db.Enroll_Emp_Pos_copy1.Find(id);
            db.Enroll_Emp_Pos_copy1.Remove(enroll_Emp_Pos_copy1);
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
