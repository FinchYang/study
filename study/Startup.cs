using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Http;

using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace study
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("inspect.json", optional: false, reloadOnChange: true)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            var logpath=System.IO.Path.Combine("logs","{Date}.txt");
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.RollingFile(logpath)//写到文本
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();
            var photo = Configuration.GetSection("photo");
            Global.PhotoPath = photo.GetSection("path").Value;
            Global.LogPhotoPath = photo.GetSection("logpath").Value;
            Global.SignaturePath = photo.GetSection("signaturepath").Value;
            var aaa = Configuration.GetSection("Host");
            Log.Information("{0},{1},{2},{3},{4}------------------------------------", aaa.Key, aaa.GetSection("Port").Value,
            Global.SignaturePath, Global.LogPhotoPath, Global.PhotoPath);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddCors(options =>
                {
                    options.AddPolicy("AnyOrigin", builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod().WithExposedHeaders("x-custom-header");;
                    });
                });
            
            services.AddMvc();
           services.Configure<MvcOptions>(options =>
    {
        options.Filters.Add(new CorsAuthorizationFilterFactory("AnyOrigin"));
    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
app.UseCors("AnyOrigin");
            app.UseMvc();
            // Configure
            
        }
    }
}
