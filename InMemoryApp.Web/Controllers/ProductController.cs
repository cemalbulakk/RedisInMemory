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
            _memoryCache.Set("zaman", DateTime.Now.ToString("F"));

            return View();
        }

        public IActionResult Show()
        {
            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
