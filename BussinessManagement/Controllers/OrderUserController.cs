using BussinessManagement.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers
{
    public class OrderUserController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: OrderUser
        public ActionResult NewOrder()
        {
            Member member = Session["Member"] as Member;
            if (member == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var order = db.Orders.Where(n => n.isCancel == false && n.CustomerID == member.ID);
            return View(order);
        }

        public ActionResult OldOrder()
        {
            Member member = Session["Member"] as Member;
            if (member == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var order = db.TheOrderDetails.Where(n => n.Order.isCancel == true || (n.Order.IsPayed == true && n.Order.Status == true) && n.Order.CustomerID == member.ID);
            return View(order);
        }

        [HttpGet]
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var orderDetails = db.TheOrderDetails.Where(n => n.OrderID == id);
            if (orderDetails == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return View(orderDetails);
        }

        [HttpPost]
        public ActionResult Cancel(int? id, FormCollection f)
        {
            var order = db.Orders.Where(n => n.IDOrder == id).SingleOrDefault();
            order.isCancel = true;
            if (ModelState.IsValid)
            {
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NewOrder");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Comment(int? idProduct)
        {
            if (idProduct == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var product = db.Products.SingleOrDefault(n => n.ID == idProduct);
            ViewBag.Product = product;
            return View();
        }

        [HttpPost]
        public ActionResult Comment([Bind(Include = "IDProduct,Content,Rate")]Comment comment)
        {
            Member member = Session["Member"] as Member;
            comment.IDMember = member.ID;
            comment.DateComment = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("OldOrder");
        }
    }
}