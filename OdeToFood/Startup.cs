using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
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
            services.AddDbContextPool<OdeToFoodDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();

            services.AddRazorPages();
            services.AddControllers();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseNodeModules();

            app.UseRouting();

            app.UseAuthorization();
            app.Use(SayHelloMiddleware);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            

        }

        private RequestDelegate SayHelloMiddleware(RequestDelegate next)
        {
            return async context =>
            {
                if(context.Request.Path.StartsWithSegments("/hello"))
                {
                    await context.Response.WriteAsync("Hello World");
                }
                else
                {
                   await  next(context);
                }
              
            };
        }
    }
}
