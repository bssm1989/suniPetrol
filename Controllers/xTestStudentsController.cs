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
    public class xTestStudentsController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: xTestStudents
        public ActionResult Index()
        {
            return View(db.xTestStudent.ToList());
        }

        // GET: xTestStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xTestStudent xTestStudent = db.xTestStudent.Find(id);
            if (xTestStudent == null)
            {
                return HttpNotFound();
            }
            return View(xTestStudent);
        }

        // GET: xTestStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: xTestStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,LastName,FirstName,EnrollmentDate")] xTestStudent xTestStudent)
        {
            if (ModelState.IsValid)
            {
                db.xTestStudent.Add(xTestStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(xTestStudent);
        }

        // GET: xTestStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xTestStudent xTestStudent = db.xTestStudent.Find(id);
            if (xTestStudent == null)
            {
                return HttpNotFound();
            }
            return View(xTestStudent);
        }

        // POST: xTestStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,LastName,FirstName,EnrollmentDate")] xTestStudent xTestStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xTestStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(xTestStudent);
        }

        // GET: xTestStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xTestStudent xTestStudent = db.xTestStudent.Find(id);
            if (xTestStudent == null)
            {
                return HttpNotFound();
            }
            return View(xTestStudent);
        }

        // POST: xTestStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            xTestStudent xTestStudent = db.xTestStudent.Find(id);
            db.xTestStudent.Remove(xTestStudent);
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
