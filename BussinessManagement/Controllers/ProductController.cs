using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers
{
    public class ProductController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductStyle1Partial()
        { 
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult ProductStyle2Partial()
        {
            return PartialView();
        }
    }
}