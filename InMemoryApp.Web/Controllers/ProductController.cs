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
            if (string.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            {
                _memoryCache.Set("zaman", DateTime.Now.ToString("F"));
            }

            // 2. Yol
            if (!_memoryCache.TryGetValue("zaman", out string zamanCache))
            {
                _memoryCache.Set("zaman", DateTime.Now.ToString("F"));
            }

            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.GetOrCreate<string>("zaman", x => DateTime.Now.ToString("F"));


            //_memoryCache.Remove("zaman");

            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
