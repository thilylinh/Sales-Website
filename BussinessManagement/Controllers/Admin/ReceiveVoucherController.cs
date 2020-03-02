using BussinessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    public class ReceiveVoucherController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: ReceiveVoucher
        [HttpGet]
        public ActionResult ImportGoods()
        {
            ViewBag.SupplierID = db.Suppliers;
        
            ViewBag.ListProduct = db.Products;
            return View();
        }
        [HttpPost]
        public ActionResult ImportGoods(ReceiveVoucher receiveVoucher, IEnumerable<ReceiveVoucherDetail> lstReceiveVoucherDetails)
        {
            ViewBag.SupplierID = db.Suppliers;
            ViewBag.ListProduct = db.Products;

            receiveVoucher.IsDeleted = false;
            db.ReceiveVouchers.Add(receiveVoucher);
            db.SaveChanges();
            Product product;
            foreach(var item in lstReceiveVoucherDetails)
            {
                //update quantity product
                product = db.Products.Single(n => n.ID==item.ProductID);
                product.Amount += item.Amount;
                 
                item.ReceiveVoucherID = receiveVoucher.ID;
            }
            db.ReceiveVoucherDetails.AddRange(lstReceiveVoucherDetails);
            db.SaveChanges();
            return View();
        }
        public ActionResult ProductOutOfStock()
        {
            var lstProductOut = db.Products.Where(n=>n.IsDeleted == false && n.Amount < 10);
            return View(lstProductOut);
        }
        [HttpGet]
        public ActionResult ImportProductSingle(int? id)
        {
            ViewBag.ProductID = new SelectList(db.Suppliers.OrderBy(n => n.Name), "ID", "Name");
            if(id== null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var product = db.Products.SingleOrDefault(n => n.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult ImportProductSingle(ReceiveVoucher receiveVoucher,ReceiveVoucherDetail receiveVoucherDetail)
        {
            ViewBag.ProductID = new SelectList(db.Suppliers.OrderBy(n => n.Name), "ID", "Name");
            receiveVoucher.IsDeleted = false;
            receiveVoucher.UpdateDate = DateTime.Now;
            db.ReceiveVouchers.Add(receiveVoucher);
            db.SaveChanges();
            receiveVoucherDetail.IDDetail = receiveVoucher.ID;
            Product product = db.Products.FirstOrDefault(n => n.ID == receiveVoucherDetail.ProductID);
            product.Amount += receiveVoucherDetail.Amount;
            db.ReceiveVoucherDetails.Add(receiveVoucherDetail);
            db.SaveChanges();
            return RedirectToAction("ProductOutOfStock");
        }

    }
}