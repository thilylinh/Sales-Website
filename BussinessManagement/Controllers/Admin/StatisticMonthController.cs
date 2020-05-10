using BussinessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    public class StatisticMonthController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: StatisticMonth
        [HttpGet]
        public ActionResult Index(FormCollection f)
        {            
            return View();        
        }
        [HttpPost]
        public ActionResult Statistic(FormCollection f)
        {
            string dateFrom = f["date-from"];
            string dateTo = f["date-to"];
            //DateTime test = Convert.ToDateTime(dateFrom);
            //ViewBag.Test = test;
            DateTime dateStart = DateTime.Parse(dateFrom);
            DateTime dateStop = DateTime.Parse(dateTo);
            ViewBag.StatisticMoney = StatisticMoney(Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo));
            ViewBag.StatisticProduct=StatisticProduct(Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo));
            ViewBag.StatisticOrder=StatisticOrder(Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo));

            ViewBag.List = db.TheOrderDetails.Where(n => n.Order.OrderDate.Value >= dateStart && n.Order.OrderDate.Value <= dateStop).ToList();
            return View();
        }
        public decimal StatisticMoney(DateTime dateFrom, DateTime dateTo)
        {
            var lstOrder = db.Orders.Where(n => n.OrderDate.Value >= dateFrom && n.OrderDate.Value <= dateTo);
            decimal total = 0;
            foreach (var item in lstOrder)
            {
                total += item.TheOrderDetails.Sum(n => n.Amount * n.Price).Value;
            }
            return total;
        }
        public int StatisticProduct(DateTime dateFrom, DateTime dateTo)
        {
            var listOrder = db.Orders.Where(n => n.OrderDate.Value >= dateFrom && n.OrderDate <= dateTo);
            int product = 0;
            foreach(var p in listOrder)
            {
                product += p.TheOrderDetails.Sum(n => n.Amount).Value;
            }
            return product;
        }
        public int StatisticOrder(DateTime dateFrom, DateTime dateTo)
        {
            return db.Orders.Where(n => n.OrderDate.Value>= dateFrom && n.OrderDate <= dateTo).Count();
        }

    }
}