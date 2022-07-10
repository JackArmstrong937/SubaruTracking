using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubaruEfficiencyTracking.Logic;

namespace SubaruEfficiencyTracking
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
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddSession();

            string RootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            RootPath = System.IO.Path.GetDirectoryName(RootPath);
            //services.AddSingleton<IDBConnector>(new SimpleFileDB("D:\\Coding\\SubaruEfficiencyTracking\\bin\\DataBase"));
            services.AddSingleton<IDBConnector>(new SimpleFileDB(RootPath+"\\WWWRoot\\Database"));
            services.AddSingleton<ITechStatService, TechStatService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
