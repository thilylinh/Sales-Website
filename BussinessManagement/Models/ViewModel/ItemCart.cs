using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BussinessManagement.Models.ViewModel
{
    public class ItemCart
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal TotalMoney { get; set; }
        public string Image { get; set; }

        public ItemCart(int id)
        {
            using(BussinessEntities db = new BussinessEntities())
            {
                this.ID = id;
                Product product = db.Products.Single(n => n.ID == id);
                this.Name = product.Name;
                this.Price = product.Price.Value;
                this.Amount = 1;
                this.TotalMoney = Price * Amount;
                this.Image = product.Image;
            }
        }
        public ItemCart()
        {

        }
    }
}