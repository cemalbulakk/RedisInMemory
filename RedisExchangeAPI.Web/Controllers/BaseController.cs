using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class BaseController : Controller
    {
        public readonly RedisService _redisService;
        public readonly IDatabase _db;

        public BaseController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(1);
        }

    }
}