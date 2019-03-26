﻿using System;
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
    public class EnrollEmpCousesController : Controller
    {
        private santisar_Entities db = new santisar_Entities();

        // GET: EnrollEmpCouses
        public ActionResult Index()
        {
            var enrollEmpCouse = db.EnrollEmpCouse
                .Include(e => e.Employee)
                .Include(e => e.EnrollCouse)
                .Include(e => e.EnrollYearSemester)
                .OrderBy(x=>x.EnrollYearSemester.YearEdu.yearName)
                .ThenBy(x=>x.EnrollCouse.CouseId);
            return View(enrollEmpCouse.ToList());
        }
        public ActionResult AddClassRoom(int? id)
        {
            EnrollEmpCouse EnEmpCoues = db.EnrollEmpCouse.Find(id);
            ViewBag.Emp = db.Employee.Find(EnEmpCoues.EnEmpId);
            ViewBag.Couse = db.EnrollCouse.Find(EnEmpCoues.EnCouseId);
            ViewBag.EnEmpCouseId = id;
            int itemGetEnrollCouseID = EnEmpCoues.EnrollCouse.EnrollCouseID ;
            ViewBag.enCouseClassAllEmp = db.EnrollEmpCouseClass.Where(x => x.EnrollEmpCouse.EnrollCouse.EnrollCouseID == itemGetEnrollCouseID&&x.Status!=0).ToList();
            return View(db.EnrollClass.Where(x => x.ClassSchoolid == EnEmpCoues.EnrollCouse.ClassInSchool.ClassID&& x.Status_class==1)
            .ToList());
        }
        [HttpPost]
        public ActionResult AddClassRoom(List<EnrollEmpCouseClass> EnrollEmpCouseClass2)
        {

            //EnrollCouse EditListEdit = db.EnrollCouse.Find(item.EnrollCouseID);
            //IEnumerable<EnrollEmpCouseClass> getFirst = EnrollEmpCouseClass.Where(x => x.EnClassId != 0 && x.EnClassId != null);
            var firstRow = EnrollEmpCouseClass2.First();
                //int itemGEtClassId = EnrollEmpCouseClass2.First().Status  ?? default(int);//EnrollClass_id
                int itemGEtClassId = firstRow.Status ?? default(int);
            int itemGetEnempCouseId=firstRow.EnEmpCouseId ?? default(int);//EnrollClass_id
            //var  getDataEnEmpCouseClass = db.EnrollEmpCouseClass.Where(y => y.EnEmpCouseId==7).ToList();
            //var getDataEnEmpCouseClass = db.EnrollEmpCouseClass.Where(x => x.EnrollClass.ClassSchoolid == itemGEtClassId ).ToList();
            var getDataEnEmpCouseClass = db.EnrollEmpCouseClass.Where(x => x.EnrollClass.ClassSchoolid == itemGEtClassId && x.EnEmpCouseId == itemGetEnempCouseId).ToList();
            //var getDataEnEmpCouseClass = db.EnrollEmpCouseClass.Where(x => x.EnrollClass.ClassSchoolid == itemGEtClassId && x.EnEmpCouseId == EnrollEmpCouseClass2.First().EnEmpCouseId).ToList();
            var countClassIdInEnEmpCouseClass = EnrollEmpCouseClass2.Where(x => x.EnClassId != 0&&x.EnClassId!=null).Count();
                if (ModelState.IsValid)
                {//มีมาก่อนแล้ว
                if (getDataEnEmpCouseClass.Count() >= countClassIdInEnEmpCouseClass)
                {
                    IEnumerable<EnrollEmpCouseClass> getdata = db.EnrollEmpCouseClass.Where(y => y.EnrollClass.ClassSchoolid == itemGEtClassId &&y.EnEmpCouseId == itemGetEnempCouseId && y.EnrollClass.Status_class==1);
                    foreach (var item in getdata)
                    {
                        if (EnrollEmpCouseClass2.Where(x => x.EnClassId == item.EnClassId).Count()>0)
                        {
                         
                            
                            item.Status = 1;
                            item.Timestamp = System.DateTime.Now;
                            
                        }
                        else
                        { item.Status = 0;
                            item.Timestamp = System.DateTime.Now;
                            

                        }
                    }
                    db.SaveChanges();
                }
                else
              
                    foreach (var item in EnrollEmpCouseClass2)
                    {
                        IEnumerable< EnrollEmpCouseClass > getData = db.EnrollEmpCouseClass.Where(x => x.EnClassId == item.EnClassId && x.EnEmpCouseId == itemGetEnempCouseId);
                        if (getData.Count()>0)
                        {
                            EnrollEmpCouseClass  getRow=db.EnrollEmpCouseClass.Find(getData.First().EnrollEmpCouseClassId);
                            getRow.Status = 1;
                            getRow.Timestamp = System.DateTime.Now;
                            
                        }
                        else
                        {if (item.EnClassId != null)
                            {

                                db.EnrollEmpCouseClass.Add(new EnrollEmpCouseClass
                                {
                                    EnClassId = item.EnClassId,
                                    EnEmpCouseId = item.EnEmpCouseId,
                                    Status = 1,
                                    Timestamp = System.DateTime.Now
                                });
                            }
                           
                        }
                    }
                db.SaveChanges();
            }
                   
            return RedirectToAction("Index");
        }
        // GET: EnrollEmpCouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            if (enrollEmpCouse == null)
            {
                return HttpNotFound();
            }
            return View(enrollEmpCouse);
        }

        // GET: EnrollEmpCouses/Create
        public ActionResult Create()
        {
            List<object> newListEmp = new List<object>();
            foreach (var member in db.Employee)
                newListEmp.Add(new
                {
                    Id = member.EmpId,
                    Name = member.EmpName + " " + member.EmpLname
                });
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester.Include(x=>x.YearEdu))
                newListYear.Add(new
                {
                    Id = year.EnrollYearSemesterId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester
                });
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse)
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name =  Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
            ViewBag.EnCouseId = new SelectList(newListCouse, "Id", "Name");
            ViewBag.EnEmpId = new SelectList(newListEmp, "Id", "Name");
            ViewBag.EnYearSemId = new SelectList(newListYear, "Id", "Name");
            ddClass();
            return View();
        }

        // POST: EnrollEmpCouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnCouseId,EnEmpCosTimestamp,EnrollEmpCouseId,EnEmpId,EnYearSemId")] EnrollEmpCouse enrollEmpCouse)
        {
            enrollEmpCouse.EnEmpCosTimestamp = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.EnrollEmpCouse.Add(enrollEmpCouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnEmpId =       new SelectList(db.Employee, "EmpId", "EmpTitle", enrollEmpCouse.EnEmpId);
            ViewBag.EnCouseId =     new SelectList(db.EnrollCouse, "EnrollCouseID", "CouseTxtId", enrollEmpCouse.EnCouseId);
            ViewBag.EnYearSemId =   new SelectList(db.EnrollYearSemester, "EnrollYearSemesterId", "EnrollYearSemesterId", enrollEmpCouse.EnYearSemId);
            return View(enrollEmpCouse);
        }

        // GET: EnrollEmpCouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            if (enrollEmpCouse == null)
            {
                return HttpNotFound();
            }
            List<object> newListEmp = new List<object>();
            foreach (var member in db.Employee)
                newListEmp.Add(new
                {
                    Id = member.EmpId,
                    Name = member.EmpName + " " + member.EmpLname
                });
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester.Include(x => x.YearEdu))
                newListYear.Add(new
                {
                    Id = year.EnrollYearSemesterId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester
                });
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse)
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name =  Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
           
            ViewBag.EnCouseId = new SelectList(newListCouse, "Id", "Name",enrollEmpCouse.EnCouseId);
            ViewBag.EnEmpId = new SelectList(newListEmp, "Id", "Name", enrollEmpCouse.EnEmpId);
            ViewBag.EnYearSemId = new SelectList(newListYear, "Id", "Name", enrollEmpCouse.EnYearSemId);
            return View(enrollEmpCouse);
        }
        public void ddClass()
        {
            List<object> newListClass = new List<object>();
            foreach (var Couse in db.ClassInSchool)
                newListClass.Add(new
                {
                    Id = Couse.ClassID,
                    Name = Couse.ClassName

                });
            ViewBag.ClassInSchool = new SelectList(newListClass, "Id", "Name");
        }
        [HttpPost]
        public JsonResult getDdEnCouse(int? id)
        {
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse.Where(x => x.ClassId==id))
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name = Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
            return Json(new SelectList(newListCouse, "Id", "Name", JsonRequestBehavior.AllowGet));
        }[HttpPost]
        public JsonResult getDdEnYear(int? id)
        {
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester)
                newListYear.Add(new
                {
                    Id = year.YearEduId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester

                });
            return Json(new SelectList(newListYear, "Id", "Name", JsonRequestBehavior.AllowGet));
        }
        // POST: EnrollEmpCouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnCouseId,EnEmpCosTimestamp,EnrollEmpCouseId,EnEmpId,EnYearSemId")] EnrollEmpCouse enrollEmpCouse)
        {
            enrollEmpCouse.EnEmpCosTimestamp = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(enrollEmpCouse).State = EntityState.Modified;
                db.SaveChanges();
                IEnumerable<EnrollEmpCouseClass> getEnrollEmpCouseClassXEMpCouseId = db.EnrollEmpCouseClass
                    .Where(x => x.EnEmpCouseId == enrollEmpCouse.EnrollEmpCouseId);
                foreach (var item in getEnrollEmpCouseClassXEMpCouseId)
                {
                    db.EnrollEmpCouseClass.Remove(item);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<object> newListEmp = new List<object>();
            foreach (var member in db.Employee)
                newListEmp.Add(new
                {
                    Id = member.EmpId,
                    Name = member.EmpName + " " + member.EmpLname
                });
            List<object> newListYear = new List<object>();
            foreach (var year in db.EnrollYearSemester.Include(x => x.YearEdu))
                newListYear.Add(new
                {
                    Id = year.EnrollYearSemesterId,
                    Name = "ปีการศึกษา " + year.YearEdu.yearName + " เทอม" + year.Semester
                });
            List<object> newListCouse = new List<object>();
            foreach (var Couse in db.EnrollCouse)
                newListCouse.Add(new
                {
                    Id = Couse.EnrollCouseID,
                    Name =  Couse.CouseTxtId + " " + Couse.Course.CourseName + " " + Couse.ClassInSchool.ClassShortName

                });
            ViewBag.EnCouseId = new SelectList(newListCouse, "Id", "Name", enrollEmpCouse.EnCouseId);
            ViewBag.EnEmpId = new SelectList(newListEmp, "Id", "Name", enrollEmpCouse.EnEmpId);
            ViewBag.EnYearSemId = new SelectList(newListYear, "Id", "Name", enrollEmpCouse.EnYearSemId);
            return View(enrollEmpCouse);
        }

        // GET: EnrollEmpCouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            if (enrollEmpCouse == null)
            {
                return HttpNotFound();
            }
            return View(enrollEmpCouse);
        }

        // POST: EnrollEmpCouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollEmpCouse enrollEmpCouse = db.EnrollEmpCouse.Find(id);
            db.EnrollEmpCouse.Remove(enrollEmpCouse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
