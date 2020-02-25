﻿using BussinessManagement.Models;
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
            List<Comment> comment = db.Comments.Where(n=> n.IDProduct==id).ToList();
            ViewBag.Comment = comment;
            
            //rating
            double ratingTotal = 0;
            int count = 0;
            int r1 = 0, r2 = 0, r3 = 0, r4 = 0, r5 = 0;
            double ratioR1 = 0, ratioR2 = 0, ratioR3 = 0, ratioR4 = 0, ratioR5 = 0;
            foreach (var i in comment)
            {
                if (i.Rate == 1)
                {
                    r1++;
                }
                if (i.Rate == 2)
                {
                    r2++;
                }
                if (i.Rate == 3)
                {
                    r3++;
                }
                if (i.Rate == 4)
                { 
                    r4++;
                }
                if (i.Rate == 5)
                {
                    r5++;
                }
                ratingTotal = (double)(ratingTotal + i.Rate);
                count++;
;            }
            ratingTotal = ratingTotal / count;
            ratioR1 = (double)((double)r1 / count) * 100;
            ratioR2 = (double)((double)r2 / count) * 100;
            ratioR3 = (double)((double)r3 / count) * 100;
            ratioR4 = (double)((double)r4 / count) * 100;
            ratioR5 = (double)((double)r5 / count) * 100;
            //
            ViewBag.R1 = r1;
            ViewBag.R2 = r2;
            ViewBag.R3 = r3;
            ViewBag.R4 = r4;
            ViewBag.R5 = r5;
            //
            ViewBag.RatioR1 = ratioR1;
            ViewBag.RatioR2 = ratioR2;
            ViewBag.RatioR3 = ratioR3;
            ViewBag.RatioR4 = ratioR4;
            ViewBag.RatioR5 = ratioR5;
            ViewBag.RateTotal = ratingTotal;
            //end rating
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

        public ActionResult ViewAllProduct(int? typeProductID, int? page)
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
            return View(lstProduct.OrderBy(n => n.Price).ToPagedList(pageCurrent, pageSize));
        }
    }
}