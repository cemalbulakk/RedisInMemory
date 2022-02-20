using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        private string HashKey { get; set; } = "hashKey";

        public HashTypeController(RedisService redisService) : base(redisService)
        {
        }

        // GET
        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            if (_db.KeyExists(HashKey))
            {
                _db.HashGetAll(HashKey).ToList().ForEach(x => { list.Add(x.Name, x.Value); });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, string val)
        {
            _db.HashSet(HashKey, name, val);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteItem(string name)
        {
            _db.HashDelete(HashKey, name);
            return RedirectToAction("Index");

        }
    }
}