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
    public class TestCoursesController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: TestCourses
        public ActionResult Index()
        {
            var testCourse = db.TestCourse.Include(t => t.TestDepartment);
            return View(testCourse.ToList());
        }

        // GET: TestCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCourse testCourse = db.TestCourse.Find(id);
            if (testCourse == null)
            {
                return HttpNotFound();
            }
            return View(testCourse);
        }

        // GET: TestCourses/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.TestDepartment, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: TestCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,DepartmentId")] TestCourse testCourse)
        {
            if (ModelState.IsValid)
            {
                db.TestCourse.Add(testCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.TestDepartment, "DepartmentId", "DepartmentName", testCourse.DepartmentId);
            return View(testCourse);
        }

        // GET: TestCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCourse testCourse = db.TestCourse.Find(id);
            if (testCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.TestDepartment, "DepartmentId", "DepartmentName", testCourse.DepartmentId);
            return View(testCourse);
        }

        // POST: TestCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,DepartmentId")] TestCourse testCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.TestDepartment, "DepartmentId", "DepartmentName", testCourse.DepartmentId);
            return View(testCourse);
        }

        // GET: TestCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCourse testCourse = db.TestCourse.Find(id);
            if (testCourse == null)
            {
                return HttpNotFound();
            }
            return View(testCourse);
        }

        // POST: TestCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestCourse testCourse = db.TestCourse.Find(id);
            db.TestCourse.Remove(testCourse);
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
