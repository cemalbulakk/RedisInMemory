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
                AbsoluteExpiration = DateTime.Now.AddSeconds(10), //Bayat data ile karşılamamak için ikisini aynı anda kullan.
                //SlidingExpiration = TimeSpan.FromSeconds(10),
                Priority = CacheItemPriority.High
            };

            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"{key} -> {value} => reason:{reason}");
            });

            _memoryCache.Set("zaman", DateTime.Now.ToString("F"), options);
            //}

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.GetOrCreate<string>("zaman", x => DateTime.Now.ToString("F"));
            //_memoryCache.Remove("zaman");

            _memoryCache.TryGetValue("zaman", out string zamanCache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = zamanCache;
            ViewBag.callback = callback;

            //ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
