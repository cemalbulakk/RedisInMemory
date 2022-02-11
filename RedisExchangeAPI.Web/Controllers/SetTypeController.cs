using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private string listKey = "names";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> namesList = new HashSet<string>();

            if (_db.KeyExists(listKey))
            {
                _db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });
            }

            return View(namesList);
        }

        public IActionResult Add(string name)
        {
            if (!_db.KeyExists(listKey))
            {
                _db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            }

            if (!string.IsNullOrEmpty(name)) _db.SetAdd(listKey, name);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await _db.SetRemoveAsync(listKey, name);

            return RedirectToAction("Index");
        }
    }
}
