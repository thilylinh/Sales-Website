using BussinessManagement.Models;
using System.Linq;
using System.Web.Mvc;

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
    }
}