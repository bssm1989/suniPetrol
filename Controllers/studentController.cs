using santisart_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace santisart_app.Controllers
{
    public class studentController : Controller
    {
        // GET: student
        santisar_Entities db = new santisar_Entities();
        public ActionResult Index()
        {

            return View(db.Students.ToList());
        }
    }
}