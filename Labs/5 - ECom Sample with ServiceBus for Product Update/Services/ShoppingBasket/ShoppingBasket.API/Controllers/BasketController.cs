using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.API.Models;
using ShoppingBasket.API.Repository;

namespace ShoppingBasket.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private IBasketRepository _repository;
        public BasketController(IBasketRepository repo)
        {
            _repository = repo;
        }
       
        // GET basket/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            return Ok(basket);
        }

        // POST basket
        [HttpPost]
        [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Basket value)
        {
            var basket = await _repository.UpdateBasketAsync(value);
            return Ok(basket);
        }

        // DELETE bakset/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.DeleteBasketAsync(id);
        }

    }
}
