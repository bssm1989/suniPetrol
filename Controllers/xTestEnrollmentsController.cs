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
    public class xTestEnrollmentsController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: xTestEnrollments
        public ActionResult Index()
        {
            var xTestEnrollment = db.xTestEnrollment.Include(x => x.xTestCourse).Include(x => x.xTestStudent);
            return View(xTestEnrollment.ToList());
        }

        // GET: xTestEnrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xTestEnrollment xTestEnrollment = db.xTestEnrollment.Find(id);
            if (xTestEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(xTestEnrollment);
        }

        // GET: xTestEnrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.xTestCourse, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.xTestStudent, "StudentID", "LastName");
            return View();
        }

        // POST: xTestEnrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID")] xTestEnrollment xTestEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.xTestEnrollment.Add(xTestEnrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.xTestCourse, "CourseID", "Title", xTestEnrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.xTestStudent, "StudentID", "LastName", xTestEnrollment.StudentID);
            return View(xTestEnrollment);
        }

        // GET: xTestEnrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xTestEnrollment xTestEnrollment = db.xTestEnrollment.Find(id);
            if (xTestEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.xTestCourse, "CourseID", "Title", xTestEnrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.xTestStudent, "StudentID", "LastName", xTestEnrollment.StudentID);
            return View(xTestEnrollment);
        }

        // POST: xTestEnrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID")] xTestEnrollment xTestEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xTestEnrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.xTestCourse, "CourseID", "Title", xTestEnrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.xTestStudent, "StudentID", "LastName", xTestEnrollment.StudentID);
            return View(xTestEnrollment);
        }

        // GET: xTestEnrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xTestEnrollment xTestEnrollment = db.xTestEnrollment.Find(id);
            if (xTestEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(xTestEnrollment);
        }

        // POST: xTestEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            xTestEnrollment xTestEnrollment = db.xTestEnrollment.Find(id);
            db.xTestEnrollment.Remove(xTestEnrollment);
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
