using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using ConfigurationProject.Data.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationProject.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var connStr = configuration["ConfigDb"];

            DatabaseHelper.CreateDatabase(connStr); 
        }

        public IConfiguration Configuration { get; }

    
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(Configuration);

            var appName = Configuration["AppName"];
            var connectionString = Configuration["ConfigDb"];
            services.AddSingleton<IConfigurationReader>(new ConfigurationReader(appName, connectionString, 10000));
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
