using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers.Admin
{
    public class OrderManagementController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // new order and pay not yet
        public ActionResult NewOrder()
        {
            var lstOrder = db.Orders.Where(n => n.IsPayed == false).OrderBy(n => n.OrderDate);
            return View(lstOrder);
        }
        // delivery not yet
        public ActionResult OrderDelivery()
        {
            var lstOrder  = db.Orders.Where(n => n.Status == false).OrderBy(n => n.OrderDate);
            return View(lstOrder);
        }
        //payed and deliveried
        public ActionResult OrderDone()
        {
            var lstOrder = db.Orders.Where(n => n.IsPayed == true && n.Status==true).OrderBy(n => n.OrderDate);
            return View(lstOrder);
        }
        [HttpGet]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order model = db.Orders.SingleOrDefault(n => n.IDOrder == id);
            
            if (model == null)
            {
                return HttpNotFound();
            }
         
            var lstOrderDetail = db.TheOrderDetails.Where(n => n.ID == id);
            ViewBag.ListOrderDetail = lstOrderDetail;
            return View(model);
        }
        [HttpPost]
        public ActionResult Approve(Order ddh)
        {
            Order ddhUpdate = db.Orders.Single(n => n.IDOrder == ddh.IDOrder);
            ddhUpdate.IsPayed = ddh.IsPayed;
            ddhUpdate.Status = ddh.Status;
            db.SaveChanges();
            var lstOrderDetail = db.TheOrderDetails.Where(n => n.ID == ddh.IDOrder);
            ViewBag.ListOrderDetail = lstOrderDetail;
            return RedirectToAction("NewOrder");
        }

    }
}