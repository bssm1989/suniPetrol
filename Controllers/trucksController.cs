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
    public class trucksController : Controller
    {
        private suniPetrolEntities db = new suniPetrolEntities();

        // GET: trucks
        public async Task<ActionResult> Index()
        {
            var trucks = db.trucks.Include(t => t.company);
            return View(await trucks.ToListAsync());
        }

        // GET: trucks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            truck truck = await db.trucks.FindAsync(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // GET: trucks/Create
        public ActionResult Create()
        {
            ViewBag.company_id = new SelectList(db.companies, "id", "Name");
            return View();
        }

        // POST: trucks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_truck,timestamp,type,car_id,company_id")] truck truck)
        {
            if (ModelState.IsValid)
            {
                db.trucks.Add(truck);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.company_id = new SelectList(db.companies, "id", "Name", truck.company_id);
            return View(truck);
        }

        // GET: trucks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            truck truck = await db.trucks.FindAsync(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            ViewBag.company_id = new SelectList(db.companies, "id", "Name", truck.company_id);
            return View(truck);
        }

        // POST: trucks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_truck,timestamp,type,car_id,company_id")] truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truck).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.company_id = new SelectList(db.companies, "id", "Name", truck.company_id);
            return View(truck);
        }

        // GET: trucks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            truck truck = await db.trucks.FindAsync(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            truck truck = await db.trucks.FindAsync(id);
            db.trucks.Remove(truck);
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
