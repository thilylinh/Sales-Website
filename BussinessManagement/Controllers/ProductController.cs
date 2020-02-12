using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers
{
    public class ProductController : Controller
    {
        BussinessEntities db = new BussinessEntities();
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
        public ActionResult ViewDetailProduct(int? productTypeID, int? producerID)
        {
            if(producerID == null || productTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstProduct = db.Products.Where(n => n.ProductTypeID == productTypeID && n.ProducerID == producerID && n.IsDeleted == false);
            if (lstProduct.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(lstProduct);
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
    }
}
