using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers
{
    public class HomeController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult MenuPartial()
        {
            var lstProduct = db.SanPhams;
            return PartialView(lstProduct);
        }
    }
}