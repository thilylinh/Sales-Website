using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers.Admin
{
    public class StatisticsController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: Statistics
        public ActionResult Index()
        {
            ViewBag.PageView = HttpContext.Application["PageView"];
            ViewBag.StatisticOrderByMonth = StatisticOrderByMonth();
            ViewBag.StatisticMoneyByMonth = StatisticMoneyByMonth();
            return View();
        }
        public int StatisticOrderByMonth()
        {
            return db.Orders.Where(n => n.OrderDate.Value.Month == DateTime.Now.Month && n.OrderDate.Value.Year == DateTime.Now.Year).Count();
            
        }
        public decimal StatisticMoneyByMonth()
        {
            var lstOrder = db.Orders.Where(n => n.OrderDate.Value.Month == DateTime.Now.Month && n.OrderDate.Value.Year == DateTime.Now.Year);
            decimal total = 0;
            foreach (var item in lstOrder)
            {
                total += item.TheOrderDetails.Sum(n => n.Amount * n.Price).Value;
            }
            return total;
        }
    }
}