﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Receives information stored in the configuration data
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();

            //Sets up the in-memory data store
            services.AddMemoryCache();

            //Registers services used to access session data
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Displays exception details
            app.UseDeveloperExceptionPage();

            // Adds simple message to HTTP responses
            app.UseStatusCodePages();

            // Enables staic content from wwwroot
            app.UseStaticFiles();
            //Associates requests with sessions when they arrive from client
            app.UseSession();

            //Enables ASP.NET Core

            app.UseMvc(routes => {
                //Setup the middleware to inspect routing. 
                //Send request to list action or the product controller
                routes.MapRoute(name: null, template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" });
                routes.MapRoute(name: null, template: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });
                routes.MapRoute(name: null, template: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });
                routes.MapRoute(name: null, template : "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                //routes.MapRoute(name: "pagination", template: "Products/Page{productPage}", defaults: new { Controller="Product", action="List"});
                //routes.MapRoute(name: "default", template: "{controller=Product}/{action=List}/{id?}");
            });

            SeedData.EnsurePopulated(app);
            
        }
    }
}
