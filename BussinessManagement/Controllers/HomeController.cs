using BussinessManagement.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
namespace BussinessManagement.Controllers
{
    public class HomeController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: Home
        public ActionResult Index()
        {
            //select list new mobile to show on home
            var lstMobile = db.Products.Where(n => n.ProductTypeID == 2 && n.IsDeleted == false && n.IsNew == 1);
            ViewBag.ListMobile = lstMobile;
            //select list new laptop to show on home
            var lstLapTop = db.Products.Where(n => n.ProductTypeID == 1 && n.IsDeleted == false && n.IsNew == 1);
            ViewBag.ListLaptop = lstLapTop;
            //select list new camera to show on home
            var lstCamera = db.Products.Where(n => n.ProductTypeID == 3 && n.IsDeleted == false && n.IsNew == 1);
            ViewBag.ListCamera = lstCamera;
            return View();
        }

        public PartialViewResult MenuPartial()
        {
            var lstProduct = db.Products;
            return PartialView(lstProduct);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include ="ID,Account,Password,Name,Address,Email,PhoneNumber")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(member);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Member member)
        {
            var ojectMember = db.Members.Where(m => m.Account == member.Account && m.Password == member.Password).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (ojectMember != null)
                {
                    Session["Member"] = ojectMember;
                    FormsAuthentication.SetAuthCookie(member.Account, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("","login fail");
            return View();
        }
        public ActionResult Logout()
        {
            Session["Member"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}
