using BussinessManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace BussinessManagement.Controllers
{
    public class ProductController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: Product
        public ActionResult ViewDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Where(n => n.ID == id && n.IsDeleted == false).SingleOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ViewDetailProduct(int? productTypeID, int? producerID, int? page)
        {
            if (producerID == null || productTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstProduct = db.Products.Where(n => n.ProductTypeID == productTypeID && n.ProducerID == producerID && n.IsDeleted == false);
            if (lstProduct.Count() == 0)
            {
                return HttpNotFound();
            }
            //paging 
            int pageSize = 9;
            int pageCurrent = (page ?? 1);
            ViewBag.ProductTypeID = productTypeID;
            ViewBag.ProducerID = producerID;
            return View(lstProduct.OrderBy(n=>n.Price).ToPagedList(pageCurrent,pageSize));
        }

        [ChildActionOnly]
        public PartialViewResult ProductStyle1Partial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult ProductStyle2Partial()
        {
            return PartialView();
        }

        public PartialViewResult Category()
        {
            var listProduct = db.Products;
            return PartialView(listProduct);
        }

        public ActionResult ViewAllProduct(int? typeProductID,int? page)
        {
            if (typeProductID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Product> lstProduct = db.Products.Where(n => n.ProductTypeID == typeProductID).ToList();

            if (lstProduct.Count() == 0)
            {
                return HttpNotFound();
            }

            //paging 
            int pageSize = 9;
            int pageCurrent = (page ?? 1);
            ViewBag.ProductTypeID = typeProductID;
            return View(lstProduct.OrderBy(n=>n.Price).ToPagedList(pageCurrent,pageSize));
        }
    }
}