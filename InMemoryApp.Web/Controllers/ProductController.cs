using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            // 1. Yol
            //if (string.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{
            //    _memoryCache.Set("zaman", DateTime.Now.ToString("F"));
            //}

            // 2. Yol
            //if (!_memoryCache.TryGetValue("zaman", out string zamanCache))
            //{
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1), //Bayat data ile karşılamamak için ikisini aynı anda kullan.
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Priority = CacheItemPriority.High
            };

            _memoryCache.Set("zaman", DateTime.Now.ToString("F"), options);
            //}

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.GetOrCreate<string>("zaman", x => DateTime.Now.ToString("F"));
            //_memoryCache.Remove("zaman");

            _memoryCache.TryGetValue("zaman", out string zamanCache);

            ViewBag.zaman = zamanCache;

            //ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
