using QLPhoThong.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Teacher.Controllers
{

    public class HomeController : Controller
    {
        // GET: Teacher/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}