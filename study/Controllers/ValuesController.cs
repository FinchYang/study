using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace study.Controllers
{
  //  [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Route("hello")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "learner", "value2" };
        }


    }
}
