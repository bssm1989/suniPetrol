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
    public class TestStudentCoursesController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: TestStudentCourses
        public ActionResult Index()
        {
            var testStudentCourse = db.TestStudentCourse.Include(t => t.TestCourse).Include(t => t.TestStudent);
            return View(testStudentCourse.ToList());
        }

        // GET: TestStudentCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestStudentCourse testStudentCourse = db.TestStudentCourse.Find(id);
            if (testStudentCourse == null)
            {
                return HttpNotFound();
            }
            return View(testStudentCourse);
        }

        // GET: TestStudentCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.TestCourse, "CourseId", "CourseName");
            ViewBag.StudentId = new SelectList(db.TestStudent, "StudentId", "FirstName");
            return View();
        }

        // POST: TestStudentCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentCourseId,CourseId,StudentId")] TestStudentCourse testStudentCourse)
        {
            if (ModelState.IsValid)
            {
                db.TestStudentCourse.Add(testStudentCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.TestCourse, "CourseId", "CourseName", testStudentCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.TestStudent, "StudentId", "FirstName", testStudentCourse.StudentId);
            return View(testStudentCourse);
        }

        // GET: TestStudentCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestStudentCourse testStudentCourse = db.TestStudentCourse.Find(id);
            if (testStudentCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.TestCourse, "CourseId", "CourseName", testStudentCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.TestStudent, "StudentId", "FirstName", testStudentCourse.StudentId);
            return View(testStudentCourse);
        }

        // POST: TestStudentCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentCourseId,CourseId,StudentId")] TestStudentCourse testStudentCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.TestCourse, "CourseId", "CourseName", testStudentCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.TestStudent, "StudentId", "FirstName", testStudentCourse.StudentId);
            return View(testStudentCourse);
        }

        // GET: TestStudentCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestStudentCourse testStudentCourse = db.TestStudentCourse.Find(id);
            if (testStudentCourse == null)
            {
                return HttpNotFound();
            }
            return View(testStudentCourse);
        }

        // POST: TestStudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestStudentCourse testStudentCourse = db.TestStudentCourse.Find(id);
            db.TestStudentCourse.Remove(testStudentCourse);
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
