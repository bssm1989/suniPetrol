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
        santisar_serv db = new santisar_serv();
        public ActionResult Index()
        {

            return View(db.STUDENTS.ToList());
        }
    }
}