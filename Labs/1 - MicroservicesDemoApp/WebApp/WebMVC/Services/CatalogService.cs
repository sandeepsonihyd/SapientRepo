using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private string _catalogServiceUrl;
        public CatalogService(IConfiguration config)
        {
            _catalogServiceUrl = config["CatalogUrl"];
        }
        public async Task<CatalogItem> GetCatalogItemAsync(int id)
        {
            var client = new HttpClient();
            string url = _catalogServiceUrl + "/CatalogItems/" + id;
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            CatalogItem item = JsonConvert.DeserializeObject<CatalogItem>(json);

            return item;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync()
        {
            var client = new HttpClient();
            string url = _catalogServiceUrl + "/CatalogItems/";
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            IEnumerable<CatalogItem> items = JsonConvert.DeserializeObject<IEnumerable<CatalogItem>>(json);
            return items;
        }
    }
}
