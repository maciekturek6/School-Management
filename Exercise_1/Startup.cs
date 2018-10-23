using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercise_1.Identity;
using Exercise_1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise_1
{
    public class Startup
    {
        public Startup()
        {
            var cb = new ConfigurationBuilder();
            cb.AddXmlFile("configuration.xml");
            Configuration = cb.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<EFCContext>(
                opt => opt.UseSqlServer(connectionString), ServiceLifetime.Transient);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<EFCContext>();

            services.AddMvc();
            //services.AddDbContext<CalendarContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CalendarContext")));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
