using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddMvc();
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

            //Enables ASP.NET Core

            app.UseMvc(routes => {
                //Setup the middleware to inspect routing. 
                //Send request to list action or the product controller
                routes.MapRoute(name: "default", template: "{controller=Product}/{action=List}/{id?}");
            });
            
        }
    }
}
