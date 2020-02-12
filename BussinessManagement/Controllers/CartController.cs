using BussinessManagement.Models;
using BussinessManagement.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BussinessManagement.Controllers
{
    public class CartController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: Cart
        public ActionResult CartView()
        {
            List<ItemCart> lstItemCart = GetCart();
            return View(lstItemCart);
        }

        public PartialViewResult CartPartial()
        {
            if (CountProduct() == 0)
            {
                ViewBag.CountProduct = 0;
                ViewBag.CountMoney = 0;
                return PartialView();
            }
            ViewBag.CountProduct = CountProduct();
            ViewBag.CountMoney = CountMoney();
            return PartialView();
        }

        public List<ItemCart> GetCart()
        {
            List<ItemCart> lstProduct = Session["Cart"] as List<ItemCart>;
            if (lstProduct == null)
            {
                lstProduct = new List<ItemCart>();
                Session["Cart"] = lstProduct;
            }
            return lstProduct;
        }

        public ActionResult AddCart(int id, string url)
        {
            Product product = db.Products.SingleOrDefault(n => n.ID == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemCart> lstCarts = GetCart();
            ItemCart productCheck = lstCarts.SingleOrDefault(n => n.ID == id);
            if (productCheck != null)
            {
                if (product.Amount < productCheck.Amount)
                {
                    return View("Notification");
                }
                productCheck.Amount++;
                productCheck.TotalMoney = productCheck.Amount * productCheck.Price;
                return Redirect(url);
            }

            ItemCart itemCart = new ItemCart(id);
            if (product.Amount < itemCart.Amount)
            {
                return View("Notification");
            }
            lstCarts.Add(itemCart);
            return Redirect(url);
        }

        public int CountProduct()
        {
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            if (itemCarts == null)
            {
                return 0;
            }
            return itemCarts.Sum(n => n.Amount);
        }

        public decimal CountMoney()
        {
            List<ItemCart> itemCarts = Session["Cart"] as List<ItemCart>;
            if (itemCarts == null)
            {
                return 0;
            }
            return itemCarts.Sum(n => n.TotalMoney);
        }

        public ActionResult EditCart(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Product product = db.Products.SingleOrDefault(n => n.ID == id);
                if (product == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    List<ItemCart> lstItemCarts = GetCart();
                    ItemCart itemCheck = lstItemCarts.SingleOrDefault(n => n.ID == id);
                    if (itemCheck == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.Cart = lstItemCarts;
                    return View(itemCheck);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(ItemCart item)
        {
            Product productCheck = db.Products.Single(n => n.ID == item.ID);
            if (productCheck.Amount < item.Amount)
            {
                return View("Notitfication");
            }

            List<ItemCart> lstItem = GetCart();

            ItemCart itemUpdate = lstItem.Find(n => n.ID == item.ID);
            itemUpdate.Amount = item.Amount;
             itemUpdate.TotalMoney = itemUpdate.Amount * itemUpdate.Price;
            return RedirectToAction("CartView");
        }
        public ActionResult DeleteCart(int id)
        {
            if(Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product product = db.Products.Single(n => n.ID == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemCart> lstItemCard = GetCart();
            ItemCart productCheck = lstItemCard.SingleOrDefault(n => n.ID == id);
            if (productCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstItemCard.Remove(productCheck);

            return RedirectToAction("CartView");
        }
    }
}