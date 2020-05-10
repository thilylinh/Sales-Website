using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers
{
    public class ProfileUserController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: ProfileUser
        public ActionResult Profile()
        {
            Customer customer = Session["Customer"] as Customer;
            return View(customer);
        }
        public ActionResult EditProfile(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                Content("<script>alert(\"Update success!\")</script>");
                return RedirectToAction("Index", "Statistics");
            }
                        
            return View();
        }
    }
}