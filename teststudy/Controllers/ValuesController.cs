using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using  Microsoft.Extensions.Logging;
namespace study.Controllers
{
    [DisableCors]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _log;
        public ValuesController(ILogger<ValuesController> log){
            _log=log;
        }
        [Route("hello")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _log.LogInformation("internal logging {0},{1}",DateTime.Now,Request.HttpContext.Connection.RemoteIpAddress);
            return new string[] { "learner", "value2" };
        }
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("yonghuxuzhi")]
        public IActionResult Yonghuxuzhi()
        {
            return View();
        }
    }
}
