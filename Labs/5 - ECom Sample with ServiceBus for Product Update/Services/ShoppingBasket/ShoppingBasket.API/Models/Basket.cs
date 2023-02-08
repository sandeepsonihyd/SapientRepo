using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.API.Models
{
    public class Basket
    {
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; }
        public Basket() { }
        public Basket(string buyerId)
        {
            BuyerId = buyerId;
            Items = new List<BasketItem>();
        }
    }

}
