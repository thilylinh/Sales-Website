using BussinessManagement.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    public class ProductManagementController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: ProductManagement
        public ActionResult Index()
        {
            var lstProduct = db.Products.Where(n => n.IsDeleted == false);
            return View(lstProduct);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Supplier = new SelectList(db.Suppliers.OrderBy(n => n.Name), "ID", "Name");
            ViewBag.ProductTyle = new SelectList(db.ProductTypes.OrderBy(n => n.NameTypeProdcut), "ID", "NameTypeProdcut");
            ViewBag.Producer = new SelectList(db.Producers.OrderBy(n => n.Name), "ID", "Name");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase[] image)
        {
            int error = 0;
            for (int i = 0; i < image.Length; i++)
            {
                if (image[i].ContentLength > 0)
                {
                    if (image[i].ContentType != "image/jpeg" && image[i].ContentType != "image/png" && image[i].ContentType != "image/gif" && image[i].ContentType != "image/jpg")
                    {
                        ViewBag.Upload += "image " + i + " is not valid";
                        error++;
                    }
                    else
                    {
                        var fileName = Path.GetFileName(image[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/ProductImage/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Error = "image is already exist";
                            error++;
                            return View();
                        }
                    }
                }
            }
            if (error > 0)
            {
                return View(product);
            }
            product.Image = image[0].FileName;
            product.Image1 = image[1].FileName;
            product.Image2 = image[2].FileName;
            product.image3 = image[3].FileName;
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}