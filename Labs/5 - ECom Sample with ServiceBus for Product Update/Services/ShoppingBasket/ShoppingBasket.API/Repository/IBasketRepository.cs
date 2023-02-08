using ShoppingBasket.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.API.Repository
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketAsync(string buyerId);
        IEnumerable<string> GetBuyers();
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string id);

    }
}
