using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {

            _db.StringSet("name", "Cemal Bulak");
            _db.StringSet("ziyaretci", 100);

            return View();
        }

        public IActionResult Show()
        {
            var value = _db.StringGet("name");
            //var value = _db.StringGetRange("name", 0, 3);
            //var value = _db.StringLength("name");
            //_db.StringIncrement("ziyaretci", 1);
            //_db.StringIncrementAsync("ziyaretci", 1);

            //_db.StringDecrementAsync("ziyaretci");

            if (value.HasValue)
            {
                ViewBag.name = value.ToString();
                //ViewBag.ziyaretci = _db.StringGet("ziyaretci");
                ViewBag.ziyaretci = _db.StringDecrementAsync("ziyaretci").Result;
            }

            return View();
        }
    }
}
