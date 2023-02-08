using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{

    [Authorize]
    public class BasketController : Controller
    {
        IBasketService _basketService;
        ICatalogService _catalogService;
        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var basket = await _basketService.GetBasket(userId);
            return View(basket);
        }
        public async Task<IActionResult> AddToBasket(int productId)
        {
            CatalogItem catItem = await _catalogService.GetCatalogItemAsync(productId);
            if (catItem != null)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                var product = new BasketItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Quantity = 1,
                    ProductName = catItem.Name,
                    PictureUrl = catItem.PictureUrl,
                    UnitPrice = catItem.Price,
                    ProductId = catItem.Id
                };
                await _basketService.AddItemToBasket(userId, product);
            }
            return RedirectToAction("Index", "Catalog");
        }
    }
}
