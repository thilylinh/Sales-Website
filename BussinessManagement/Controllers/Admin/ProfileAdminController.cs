using BussinessManagement.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    public class ProfileAdminController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: ProfileAdmin
        public ActionResult Profile()
        {
            Member member = Session["Member"] as Member;
            return View(member);
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