using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BlogWebsite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;

namespace BlogWebsite
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache();
            services.AddDbContext<InfiniteBlogDBContext>(option => option.UseSqlServer("Server=.\\SQLExpress;Database=InfiniteBlogDB;Trusted_Connection=True;"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(option => {
                   //option.LoginPath = "/User/LogIn";
                   //option.AccessDeniedPath = "/Error/AccessDenied";
                   //option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                   option.Events.OnRedirectToLogin = (context) =>
                  {
                      context.Response.StatusCode = 401;
                      return Task.CompletedTask;
                  };

              });

            services.AddPaging();
            services.AddSession(options => {
                //options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseExceptionHandler("/Error/Index");
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            //  app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {




                //routes.MapRoute(
                //    name: "",
                //    template: "{controller=Start}/{action=Welcome}/{id?}");

                //routes.MapRoute(
                // name: "",
                // template: "{Controller}/{Cid}",
                // defaults: new { Controller = "Channel", action = "MyChannel" }
                // );




                //routes.MapRoute(
                //    name: "ChannelPannel",
                //    template: "{controller=Channel}/{action=ChannelPanel}/{id}");




                //routes.MapRoute(
                //    name: "",
                //    template: "{Controller}/{Cid}/{Did?}/{Tid?}",
                //    defaults: new { Controller = "Channel", action = "Thread" }
                //    );




                //routes.MapRoute(
                //    name:"mychannel",
                //    template: "{controller}/{id}",
                //    defaults: new {Controller="Channel",action= "MyChannel"}
                //    );
                routes.MapRoute(
                    name:"",
                    template:"{Controller}/{action}/{Cid}/{Did?}/{Tid?}",
                    defaults:new { Controller = "Channel", action = "MyChannel" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Start}/{action=Welcome}/{id?}");
           

            });
        }
    }
}
