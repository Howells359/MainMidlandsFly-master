
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MainMidlandsFly.Data;
using MainMidlandsFly.Models;
using MainMidlandsFly.Services;


namespace MainMidlandsFly
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddDbContext<AircraftContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));
                       
            services.AddDbContext<MainFlightContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));

            services.AddDbContext<NewAircraftContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));

            //services.AddDbContext<NewFlightsContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));

            services.AddDbContext<CrewContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));

            services.AddDbContext<AircraftMaintenanceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AirlineAppDatabase")));
            services.AddSession();

            AllocationScheduler.Start();
            //  AllocateSchedulerNew.Start();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

          

        }
    }
}
