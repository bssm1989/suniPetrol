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
    public class TestStudentsController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: TestStudents
        public ActionResult Index()
        {
            return View(db.TestStudent.ToList());
        }

        // GET: TestStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestStudent testStudent = db.TestStudent.Find(id);
            if (testStudent == null)
            {
                return HttpNotFound();
            }
            return View(testStudent);
        }

        // GET: TestStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Phone")] TestStudent testStudent)
        {
            if (ModelState.IsValid)
            {
                db.TestStudent.Add(testStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testStudent);
        }

        // GET: TestStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestStudent testStudent = db.TestStudent.Find(id);
            if (testStudent == null)
            {
                return HttpNotFound();
            }
            return View(testStudent);
        }

        // POST: TestStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Phone")] TestStudent testStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testStudent);
        }

        // GET: TestStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestStudent testStudent = db.TestStudent.Find(id);
            if (testStudent == null)
            {
                return HttpNotFound();
            }
            return View(testStudent);
        }

        // POST: TestStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestStudent testStudent = db.TestStudent.Find(id);
            db.TestStudent.Remove(testStudent);
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
