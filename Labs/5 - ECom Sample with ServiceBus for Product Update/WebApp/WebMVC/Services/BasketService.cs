using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Utils;

namespace WebMVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly string _remoteServiceBaseUrl;
        HttpContext _httpContext;
        ICustomHttpClient _httpClient;
        public BasketService(IConfiguration config, IHttpContextAccessor httpContextAccessor, ICustomHttpClient httpClient)
        {
            _remoteServiceBaseUrl = config["ShoppingCartUrl"];
            _httpContext = httpContextAccessor.HttpContext;
            _httpClient = httpClient;
        }

        public async Task AddItemToBasket(string userId, BasketItem product)
        {
            var basket = await GetBasket(userId);
            var basketItem = basket.Items
                .Where(p => p.ProductId == product.ProductId)
                .FirstOrDefault();
            if (basketItem == null)
            {
                basket.Items.Add(product);
            }
            else
            {
                basketItem.Quantity += 1;
            }
            await UpdateBasket(basket);
        }

        public async Task ClearBasket(string userId)
        {
            //var client = new HttpClient();
            string token = await _httpContext.GetTokenAsync("access_token");
            await _httpClient.DeleteAsync(_remoteServiceBaseUrl + "/basket/" + userId);
        }

        public async Task<Basket> GetBasket(string userId)
        {
            string token = await _httpContext.GetTokenAsync("access_token");
            var basket = await _httpClient.GetAsync<Basket>(_remoteServiceBaseUrl + "/basket/" + userId, token);   
            if (basket == null)
            {
                basket = new Basket()
                {
                    BuyerId = userId,
                    Items = new List<BasketItem>()
                };
            }
            return basket;
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            string token = await _httpContext.GetTokenAsync("access_token");
           // HttpContent content = new StringContent(JsonConvert.SerializeObject(basket), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_remoteServiceBaseUrl + "/basket/", basket, token);
            response.EnsureSuccessStatusCode();
            return basket;
        }
    }
}
