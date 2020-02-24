using BussinessManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers
{
    public class ViewOrderController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: ViewOrder
        public ActionResult NewOrder()
        {
            Member member = Session["Member"] as Member;
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Order> lstNewOrder = db.Orders.Where(n => n.isCancel == false && n.CustomerID == member.ID).ToList();
            List<TheOrderDetail> lst = null;
            foreach (var item in lstNewOrder)
            {
                lst.AddRange(db.TheOrderDetails.Where(n => n.OrderID == item.IDOrder).ToList());
            }
            
            return View(lst);
        }

        public ActionResult OldOrder()
        {
            Member member = Session["Member"] as Member;
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var lstOldOrder = db.Orders.Where(n => n.isCancel == true || (n.IsPayed == true && n.CustomerID == member.ID));
            List<TheOrderDetail> lst = null;
            foreach (var item in lstOldOrder)
            {
                lst.AddRange(db.TheOrderDetails.Where(n => n.OrderID == item.IDOrder).ToList());
            }
            return View(lst);
        }
    }
}