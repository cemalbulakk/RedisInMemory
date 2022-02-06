using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
            };

            //_distributedCache.SetString("name", "Cemal", cacheEntryOptions);
            //await _distributedCache.SetStringAsync("name", "Göktuğ");

            var product = new Product()
            {
                Id = 1,
                Name = "Kitap",
                Price = 50
            };

            string jsonProduct1 = JsonConvert.SerializeObject(product);
            //await _distributedCache.SetStringAsync("product:1", jsonProduct1, cacheEntryOptions);

            var byteProduct = Encoding.UTF8.GetBytes(jsonProduct1);

            await _distributedCache.SetAsync("product:1", byteProduct, cacheEntryOptions);

            return View();
        }

        public IActionResult Show()
        {
            //var name = _distributedCache.GetString("name");
            //ViewBag.name = name;

            var jsonProduct = _distributedCache.Get("product:1");

            //var product = JsonConvert.DeserializeObject<Product>(jsonProduct);
            var byteProduct
                = Encoding.UTF8.GetString(jsonProduct);
            var product = JsonConvert.DeserializeObject<Product>(byteProduct);

            return View(product);
        }

        public IActionResult Remove()
        {
            _distributedCache.Remove("name");

            return View();
        }
    }
}
