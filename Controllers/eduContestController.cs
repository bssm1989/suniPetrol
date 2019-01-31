using santisart_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace santisart_app.Controllers
{
    public class eduContestController : Controller
    {
        // GET: eduContest
        santisar_Entities db = new santisar_Entities();
        public ActionResult Index()
        {

            return View(db.studentEduContest2561);
        }

        // GET: eduContest/Details/5
        public ActionResult Details(int id)
        {
            return View(db.studentEduContest2561.Where(x=>x.EduContest_id==id));
        }

        // GET: eduContest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: eduContest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: eduContest/Edit/5
        public ActionResult Edit(int id)
        {
          return View(db.studentEduContest2561.Where(x => x.EduContest_id == id).FirstOrDefault());
        }

        // POST: eduContest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: eduContest/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.studentEduContest2561.Where(x => x.EduContest_id == id));
        }

        // POST: eduContest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
