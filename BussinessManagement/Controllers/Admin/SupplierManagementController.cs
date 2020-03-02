using BussinessManagement.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    public class SupplierManagementController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: SupplierManagement
        public ActionResult Index()
        {
            var supplier = db.Suppliers;
            return View(supplier);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var suplier = db.Suppliers.SingleOrDefault(n => n.ID == id);
            if (suplier == null)
            {
                return HttpNotFound();
            }
            return View(suplier);
        }

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }
    }
}