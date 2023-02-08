using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
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

        [Authorize()]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _catalogService.GetCatalogItemAsync(id);
            return View(item);
        }

        [HttpPost]
        [Authorize()]
        public async Task<IActionResult> Edit(int id, CatalogItem item)
        {
            await _catalogService.Update(item);
            return RedirectToAction("Index");
        }
    }
}
