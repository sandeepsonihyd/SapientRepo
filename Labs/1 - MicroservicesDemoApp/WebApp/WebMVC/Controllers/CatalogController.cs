using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        ICatalogService _catalogService;
        public CatalogController(ICatalogService service)
        {
            _catalogService = service;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _catalogService.GetCatalogItemsAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _catalogService.GetCatalogItemAsync(id);
            return View(item);
        }
    }
}
