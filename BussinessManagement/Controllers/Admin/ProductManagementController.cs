using BussinessManagement.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    [Authorize(Roles = "ManageProduct")]
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
            ViewBag.Supplier = new SelectList(db.Suppliers.OrderBy(n => n.Name), "ID", "Name");
            ViewBag.ProductTyle = new SelectList(db.ProductTypes.OrderBy(n => n.NameTypeProdcut), "ID", "NameTypeProdcut");
            ViewBag.Producer = new SelectList(db.Producers.OrderBy(n => n.Name), "ID", "Name");
            int error = 0;
            for (int i = 0; i < image.Count(); i++)
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
                        var path = Path.Combine(Server.MapPath("~/Content/ProductImage"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Error = "image is already exist";
                            return View();
                        }
                        else
                        {
                            image[i].SaveAs(path);
                        }
                    }
                }
            }
            
            product.Image = image[0].FileName;
            product.Image1 = image[1].FileName;
            product.Image2 = image[2].FileName;
            product.image3 = image[3].FileName;
            db.Products.Add(product);
            db.SaveChanges();
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
            var product = db.Products.SingleOrDefault(n => n.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            } 
            ViewBag.Supplier = new SelectList(db.Suppliers.OrderBy(n => n.Name), "ID", "Name",product.SupplierID);
            ViewBag.ProductTyle = new SelectList(db.ProductTypes.OrderBy(n => n.NameTypeProdcut), "ID", "NameTypeProdcut",product.ProductTypeID);
            ViewBag.Producer = new SelectList(db.Producers.OrderBy(n => n.Name), "ID", "Name",product.ProducerID);


            return View(product);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var product = db.Products.SingleOrDefault(n => n.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}