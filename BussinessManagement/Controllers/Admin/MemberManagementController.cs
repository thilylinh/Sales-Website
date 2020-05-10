using BussinessManagement.Models;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    [Authorize(Roles = "Resgister")]
    public class MemberManagementController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: MemberManagement
        public ActionResult Index()
        {
            var member = db.Members.ToList();
            return View(member);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ListMemberType = new SelectList(db.MemberTypes.OrderBy(n => n.IDTypeMember), "IDTypeMember", "NameType");
            return View();
        }

        [HttpPost]
        public ActionResult Add(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.ListMemberType = new SelectList(db.MemberTypes.OrderBy(n => n.IDTypeMember), "IDTypeMember", "NameType");
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var member = db.Members.SingleOrDefault(n => n.ID == id);
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                var member = db.Members.SingleOrDefault(n => n.ID == id);
                db.Members.Remove(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}