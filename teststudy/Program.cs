using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;
namespace study
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder().UseUrls("http://*:8001")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
            
                .UseStartup<Startup>()
              //  .UseUrls("http://localhost:5000")
                .Build();
             
               

            host.Run();
        }
    }
}
