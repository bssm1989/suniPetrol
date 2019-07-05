using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using santisart_app.Models;

namespace santisart_app.Controllers
{
    public class Enroll_reportTruckKpiController : Controller
    {
        private suniPetrolEntities db = new suniPetrolEntities();

        // GET: Enroll_reportTruckKpi
        public async Task<ActionResult> Index()
        {
            var enroll_reportTruckKpi = db.Enroll_reportTruckKpi.Include(e => e.truck);
            return View(await enroll_reportTruckKpi.ToListAsync());
        }

        // GET: Enroll_reportTruckKpi/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll_reportTruckKpi enroll_reportTruckKpi = await db.Enroll_reportTruckKpi.FindAsync(id);
            if (enroll_reportTruckKpi == null)
            {
                return HttpNotFound();
            }
            return View(enroll_reportTruckKpi);
        }

        // GET: Enroll_reportTruckKpi/Create
        public ActionResult Create()
        {
            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck");
            return View();
        }

        // POST: Enroll_reportTruckKpi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_report,kpi,date,meter_last,petrol_last,timestamp,car_id")] Enroll_reportTruckKpi enroll_reportTruckKpi)
        {
            if (ModelState.IsValid)
            {
                db.Enroll_reportTruckKpi.Add(enroll_reportTruckKpi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck", enroll_reportTruckKpi.car_id);
            return View(enroll_reportTruckKpi);
        }

        // GET: Enroll_reportTruckKpi/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll_reportTruckKpi enroll_reportTruckKpi = await db.Enroll_reportTruckKpi.FindAsync(id);
            if (enroll_reportTruckKpi == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck", enroll_reportTruckKpi.car_id);
            return View(enroll_reportTruckKpi);
        }

        // POST: Enroll_reportTruckKpi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_report,kpi,date,meter_last,petrol_last,timestamp,car_id")] Enroll_reportTruckKpi enroll_reportTruckKpi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enroll_reportTruckKpi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.car_id = new SelectList(db.trucks, "car_id", "id_truck", enroll_reportTruckKpi.car_id);
            return View(enroll_reportTruckKpi);
        }

        // GET: Enroll_reportTruckKpi/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll_reportTruckKpi enroll_reportTruckKpi = await db.Enroll_reportTruckKpi.FindAsync(id);
            if (enroll_reportTruckKpi == null)
            {
                return HttpNotFound();
            }
            return View(enroll_reportTruckKpi);
        }

        // POST: Enroll_reportTruckKpi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enroll_reportTruckKpi enroll_reportTruckKpi = await db.Enroll_reportTruckKpi.FindAsync(id);
            db.Enroll_reportTruckKpi.Remove(enroll_reportTruckKpi);
            await db.SaveChangesAsync();
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
