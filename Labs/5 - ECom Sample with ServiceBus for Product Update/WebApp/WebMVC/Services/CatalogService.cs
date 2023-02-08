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
    public class CatalogService : ICatalogService
    {
        private ICustomHttpClient _httpClient;
        private string _catalogServiceUrl;
        private HttpContext httpContext;
        public CatalogService(IConfiguration config, ICustomHttpClient httpClient, IHttpContextAccessor httpContextAccessor )
        {
            _catalogServiceUrl = config["CatalogUrl"];
            _httpClient = httpClient;
            httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<CatalogItem> GetCatalogItemAsync(int id)
        {
            //var client = new HttpClient();
            string url = _catalogServiceUrl + "/CatalogItems/" + id;
            string token = await _httpClient.GetClientCredentailToken("catalog");
            CatalogItem item = await _httpClient.GetAsync<CatalogItem>(url,token);
            //var json = await response.Content.ReadAsStringAsync();
           // CatalogItem item = JsonConvert.DeserializeObject<CatalogItem>(json);
            return item;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync()
        {
            //var client = new HttpClient();
            string url = _catalogServiceUrl + "/CatalogItems/";
            //var response = await client.GetAsync(url);
            //var json = await response.Content.ReadAsStringAsync();
            //IEnumerable<CatalogItem> items = JsonConvert.DeserializeObject<IEnumerable<CatalogItem>>(json);
            string token = await _httpClient.GetClientCredentailToken("catalog");
            IEnumerable<CatalogItem> items = await _httpClient.GetAsync<IEnumerable<CatalogItem>>(url,token);
            return items;
        }

        public async Task Update(CatalogItem item)
        {
            string url = _catalogServiceUrl + "/CatalogItems/" + item.Id;
            var token = await httpContext.GetTokenAsync("access_token");
            var response = await _httpClient.PutAsync<CatalogItem>(url, item, token);
            response.EnsureSuccessStatusCode();
        }
    }
}
