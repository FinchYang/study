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
            //  Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()//等级
            //    .WriteTo.LiterateConsole()//写到控制台
            //    .WriteTo.RollingFile("logs\\{Date}.txt")//写到文本
            //    .CreateLogger();

            // Log.Information("Hello, world!");
            //  Log.CloseAndFlush();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
            
                .UseStartup<Startup>()
              //  .UseUrls("http://192.168.10.94:5000")
                  .UseUrls("http://localhost:5000")
                .Build();
             
               

            host.Run();
        }
    }
}
