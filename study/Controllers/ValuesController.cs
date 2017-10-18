using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using  Microsoft.Extensions.Logging;
using System.IO;

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
        public class commonresponse
        {
            public int status { get; set; }
            public string content { get; set; }
        }
        public class labels
        {
            public string label { get; set; }
        }
        public class values
        {
            public string value { get; set; }
        }
        public class aballresponse : commonresponse
        {
            public List<values> values { get; set; }
            public List<labels> labels { get; set; }
        }
        [Route("CheckBorder")]
        [HttpGet]
        public aballresponse CheckBorder()
        {
            var ret = new aballresponse { status = 0, values = new List<values>(), labels = new List<labels>() };
            try
            {
                var getpath = "/home/inspect/ftp/get/back";
                var gt = new DirectoryInfo(getpath).GetFiles().Where(a => a.Name.Contains("studentmessage"));

                var aaaaa = from one in gt
                            group one by one.Name.Substring(0, 10) into onegroup
                            orderby onegroup.Key descending
                            select new  { day = onegroup.Key, count = onegroup.Count() };
                foreach (var cc in aaaaa)
                {
                    ret.labels.Add(new labels { label = cc.day });
                    ret.values.Add(new values { value = cc.count.ToString() });
                }
            }
            catch (Exception ex)
            {
                ret.content += ex.Message;
            }

            return ret;
        }
    }
}
