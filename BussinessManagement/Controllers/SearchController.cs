using BussinessManagement.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers
{
    public class SearchController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: Search
        [HttpGet]
        public ActionResult Search(string searchString, int? page)
        {
            //paging
            int pageSize = 9;
            int pageCurrent = (page ?? 1);
            //query search
            var lstProduct = db.Products.Where(n => n.Name.Contains(searchString));
            ViewBag.Values = searchString;
            //return View(lstProduct);
            return View(lstProduct.OrderBy(n => n.Name).ToPagedList(pageCurrent, pageSize));
        }

        [HttpPost]
        public ActionResult GetStringSearch(string searchString)
        {
            return RedirectToAction("Search", new { @searchString = searchString });
        }
    }
}