using System;
using System.IO;
using DisplayLog.Models;
using DisplayLog.Web.Filter;
using DisplayLog.Web.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SV.WebExtension.Core;
using Swashbuckle.AspNetCore.Swagger;

namespace DisplayLog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(optioins =>
            {
                optioins.Filters.Add<CustomExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors();

            services.AddSignalR(options=>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(30);
                options.EnableDetailedErrors = true;
            });

            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddAutoFactory("DisplayLog.Services");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "DisplayLog Api",
                    Description = "The all DisplayLog Api",
                    TermsOfService = "None"
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DisplayLog.Web.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile("Logs/{Date}.txt")
                .CreateLogger();

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Add("index.html");    //将index.html改为需要默认起始页的文件名.
            app.UseDefaultFiles(options);
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //判断是否为开发环境
            if (env.IsDevelopment())
            {
                
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DisplayLog Api v1");
            });

            //配置跨域处理
            app.UseCors(builder =>
            {
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials(); // 允许跨域Cookies
                builder.AllowAnyOrigin();   // 允许任何来源的主机访问
                builder.WithOrigins("http://localhost:8077", "http://localhost:8080");
            });

            app.UseSignalR(route =>
            {
                route.MapHub<PushLogHub>("/pushLogHub");
            });

            app.UseMvc();

        }
    }
}
