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
            var lstMobile = db.Products.Where(n => n.ProductTypeID == 2 && n.IsDeleted == false && n.IsNew == true);
            ViewBag.ListMobile = lstMobile;
            //select list new laptop to show on home
            var lstLapTop = db.Products.Where(n => n.ProductTypeID == 1 && n.IsDeleted == false && n.IsNew == true);
            ViewBag.ListLaptop = lstLapTop;
            //select list new camera to show on home
            var lstCamera = db.Products.Where(n => n.ProductTypeID == 3 && n.IsDeleted == false && n.IsNew == true);
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
        public ActionResult Register([Bind(Include = "ID,Name,Address,Email,PhoneNumber,Account,Pass")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(customer);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            var cus = db.Customers.Where(m => m.Account == customer.Account && m.Pass == customer.Pass).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (cus != null)
                {
                    Session["Customer"] = cus;
                    FormsAuthentication.SetAuthCookie(cus.Account, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "login fail");
            return View();
        }

        public ActionResult Logout()
        {
            Session["Customer"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}