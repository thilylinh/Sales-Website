using BussinessManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    [Authorize(Roles = "Authorization")]
    public class AuthorizationController : Controller
    {
        // GET: Authen
        private BussinessEntities db = new BussinessEntities();

        public ActionResult Index()
        {
            return View(db.MemberTypes.OrderBy(n => n.NameType));
        }

        [HttpGet]
        public ActionResult Authorization(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            MemberType memberType = db.MemberTypes.SingleOrDefault(n => n.IDTypeMember == id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = db.Roles.ToList();
            ViewBag.MemberTypeRole = db.MemberType_Role.Where(n => n.MemberTypeID == id).ToList();
            return View(memberType);
        }

        [HttpPost]
        public ActionResult Authorization(int? memberTypeID, IEnumerable<MemberType_Role> lstAuthori)
        {
            var lstAuthorizated = db.MemberType_Role.Where(n => n.MemberTypeID == memberTypeID);
            if (lstAuthorizated.Count() != 0)
            {
                db.MemberType_Role.RemoveRange(lstAuthorizated);
                db.SaveChanges();
            }
            if (lstAuthori != null)
            {
                foreach (var item in lstAuthori)
                {
                    item.MemberTypeID = int.Parse(memberTypeID.Value.ToString());
                    db.MemberType_Role.Add(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}